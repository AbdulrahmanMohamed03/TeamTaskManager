using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
             _context.Set<T>().Add(entity);
            return entity;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }


        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
