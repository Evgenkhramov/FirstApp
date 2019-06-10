using FirstApp.Core.Entities;
using System;

namespace FirstApp.Core.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity, new()
    {
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
