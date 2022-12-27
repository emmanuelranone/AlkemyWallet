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
        public async Task<T> GetById(int id)
        {
            var entity = await _entities.FindAsync(id);
            if ((entity == null))
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> Delete(int id)
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

        public IEnumerable<T> GetAll()
        {
            List<T> entities = new List<T>();
            entities = _entities.ToList();
            return entities;
        }


        public async Task<T> Update(T entity)
        {
            var result = _dbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public async Task<T> Add(T entity)
        {
            var result = await _entities.AddAsync(entity);
            return result.Entity;
        }
    }
}
