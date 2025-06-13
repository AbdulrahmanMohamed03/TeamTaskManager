using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Helpers;
using TeamTaskManager.Core.Models;
using TeamTaskManager.Core.Services.Interfaces;

namespace TeamTaskManager.Core.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _Jwt;
        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _Jwt = jwt.Value;
        }

        public async Task<RoleDTO> AddRole(string roleName)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);
            if (role) {
                return new RoleDTO { message = "Role already exists"};
            }
            var rol = new IdentityRole(roleName);
            await _roleManager.CreateAsync(rol);
            return new RoleDTO { RoleName = roleName };
        }

        public async Task<AssignRoleDTO> AssignToRole(AssignRoleDTO assignRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDTO.UserId);
            if (user == null) {
                return new AssignRoleDTO { message = "User Not Found!" };
            }
            var role = await _roleManager.FindByNameAsync(assignRoleDTO.RoleName);
            if (role == null)
            {
                return new AssignRoleDTO { message = "Role Not Found!" };
            }
            var exist = await _userManager.IsInRoleAsync(user, role.Name);
            if (exist) {
                return new AssignRoleDTO { message = "This User already assigned to this role" };
            }
            await _userManager.AddToRoleAsync(user, assignRoleDTO.RoleName);
            return assignRoleDTO;
        }

        public async Task<TokenDTO> Login(LoginDTO loginDTO)
        {
            string wrongInput = "Username, Email, Or password is wrong!";
            var user = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);

            if (user == null)
                return new TokenDTO { Message = wrongInput };

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
                return new TokenDTO { Message = wrongInput };

            var JWTtoken = await CreateToken(user);
            string token = new JwtSecurityTokenHandler().WriteToken(JWTtoken);
            var tokenDTO = new TokenDTO { Token = token, ExpireDate = JWTtoken.ValidTo };

            var activeRefreshToken = user.RefreshTokens.FirstOrDefault();
            if (activeRefreshToken == null)
            {
                var refreshToken = GenerateRefreshToken();
                tokenDTO.RefreshToken = refreshToken.Token;
                tokenDTO.RefreshTokenExpiresOn = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                tokenDTO.RefreshToken = activeRefreshToken.Token;
                tokenDTO.RefreshTokenExpiresOn = activeRefreshToken.ExpiresOn;
            }
            return tokenDTO;
        }

        public async Task<TokenDTO> Register(RegisterDTO registerDTO)
        {
            if(await _userManager.FindByEmailAsync(registerDTO.Email) is not null || await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                return new TokenDTO { Message = "Username or Email already exists!" };
            }
            User user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) { 
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new TokenDTO { Message = errors };
            }

            //Assign Employee Role To Each Member
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new TokenDTO { Message = errors };
            }

            // Create Token
            var JWTtoken = await CreateToken(user);
            string token = new JwtSecurityTokenHandler().WriteToken(JWTtoken);

            //RefeshToken
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            return new TokenDTO { Token = token, ExpireDate = JWTtoken.ValidTo, RefreshToken = refreshToken.Token, RefreshTokenExpiresOn = refreshToken.ExpiresOn
            };

        }


        private async Task<JwtSecurityToken> CreateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var UserRoles = await _userManager.GetRolesAsync(user);
            var UserRoleClaims = new List<Claim>();
            foreach (var userRole in UserRoles)
            {
                UserRoleClaims.Add(new Claim("role", userRole));
            }

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("uname", user.UserName),
            }.Union(userClaims).Union(UserRoleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(_Jwt.DurationInDays),
                signingCredentials: signingCredentials
                );

            return token;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var random = new byte[32];
            RandomNumberGenerator.Fill(random);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(random),
                CreatedOn = DateTime.Now,
                ExpiresOn = DateTime.Now.AddDays(5),
            };
        }
    }
}
