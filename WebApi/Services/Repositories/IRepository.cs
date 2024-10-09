namespace WebApi.Services.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T?> GetById(Guid id);

        public Task<int> Insert(T entity);

        public Task<int> Delete(T entity);

        public Task<int> Update(T entity);
    }
}
