namespace WebApi.Services.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T?> GetById(Guid id);

        public Task<bool> Insert(T entity);

        public Task<bool> Delete(T entity);

        public Task<bool> Update(T entity);

        public bool Exists(Guid id);
    }
}
