namespace WebApi.Entities.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task Insert(T entity);

        public Task Delete(T entity);

        public Task Update(T entity);
    }
}
