using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();      
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteSoft(int id);
        Task<int> Delete(int id);
    }
}
