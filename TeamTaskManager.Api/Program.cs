using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TeamTaskManager.Core;
using TeamTaskManager.Core.Helpers;
using TeamTaskManager.Core.Models;
using TeamTaskManager.Core.Services.Implementation;
using TeamTaskManager.Core.Services.Interfaces;
using TeamTaskManager.EF;
using TeamTaskManager.EF.Context;
using Task = System.Threading.Tasks.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService,ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
});
builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
}
    );

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
//builder.Services.AddAuthorization();


//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeamTaskManager API",
        Version = "v1"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndAdmin(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamTaskManager API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

    string[] roleNames = { "admin"};

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new User
        {
            UserName = "admin",
            Email = adminEmail,
            FirstName = "System",
            LastName = "Admin"
        };

        var result = await userManager.CreateAsync(user, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "admin");
        }
    }
    else
    {
        var isInRole = await userManager.IsInRoleAsync(adminUser, "admin");
        if (!isInRole)
        {
            await userManager.AddToRoleAsync(adminUser, "admin");
        }
    }
}
