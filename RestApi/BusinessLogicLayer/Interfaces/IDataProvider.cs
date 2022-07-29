using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDataProvider<T>
    {
        List<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
