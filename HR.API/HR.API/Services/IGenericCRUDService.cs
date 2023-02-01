using HR.DataAccess.Entities;

namespace HR.API.Services
{
    public interface IGenericCRUDService<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<T> Create(T template);
        public Task<T> Update(int id, T template);
        public Task<bool> Delete(int id);
    }
}
