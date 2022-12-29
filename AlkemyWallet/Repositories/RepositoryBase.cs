using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly WalletDbContext _dbContext;
        protected readonly DbSet<T> _entities;

        public RepositoryBase(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if ((entity == null))
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> DeleteSoft(int id)
        {
            try{
                var entity = await _entities.FindAsync(id);
                if (entity == null || entity.IsDeleted)
                {
                    return false;
                }else{
                    entity.IsDeleted = true;               

                    _entities.Update(entity);
                    return true;
                }                
            }
            catch (Exception e){
                throw new Exception("Error on Repository.Delete", e);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);
                if(entity == null){
                    return 0;
                }else{
                    _entities.Remove(entity);
                    return id;
                }                
            }
            catch (Exception e)
            {
                throw new Exception("Error on Repository.Delete", e);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }


        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _entities.AddAsync(entity);
            return result.Entity;
        }
    }
}
