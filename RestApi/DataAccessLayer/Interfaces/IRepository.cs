using System;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
