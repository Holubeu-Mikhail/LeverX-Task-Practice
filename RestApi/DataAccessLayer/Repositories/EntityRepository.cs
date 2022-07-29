using System;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataAccessLayer.IntegrationTests")]
[assembly: InternalsVisibleTo("ProductsApi")]

namespace DataAccessLayer.Repositories
{
    internal class EntityRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DbContext _context;

        public EntityRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            var result = _context.Set<T>().ToList().AsQueryable();
            return result;
        }

        public T Get(Guid id)
        {
            var result = _context.Set<T>().Find(id);
            return result;
        }

        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var item = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }
    }
}
