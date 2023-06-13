namespace WebApi.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetOne(long id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(long id);
    }
}
