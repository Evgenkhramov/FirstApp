using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;

namespace FirstApp.Core.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly SQLiteConnection _connect;

        public BaseRepository(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<T>();
        }

        public T GetById(int id)
        {
            T item = _connect.Table<T>().Where(i => i.Id == id).FirstOrDefault();
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
            throw new System.NotImplementedException();
        }
    }
}
