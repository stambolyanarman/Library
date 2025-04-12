namespace Library.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreatAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }

}
