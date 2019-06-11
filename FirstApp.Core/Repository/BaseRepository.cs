using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;

namespace FirstApp.Core.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly SQLiteConnection _connect;
        protected readonly TableQuery<T> _table;

        public BaseRepository(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<T>();

            _table = _connect.Table<T>();
        }

        public T GetById(int id)
        {
            T item = _table.Where(i => i.Id == id).FirstOrDefault();

            return item;
        }
        public void Insert(T entity)
        {
            _connect.Insert(entity);
        }

        public void Update(T entity)
        {
            _connect.Update(entity);
        }

        public void Delete(T entity)
        {
            _connect.Delete(entity);
        }

        public void Dispose()
        {

        }
    }
}
