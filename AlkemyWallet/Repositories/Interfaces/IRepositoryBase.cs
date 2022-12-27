using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> Add(T entity);
        IEnumerable<T> GetAll();      
        Task<T> GetById(int id);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        
    }
}
