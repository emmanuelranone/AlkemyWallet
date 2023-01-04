using AlkemyWallet.Core.Helper;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                throw new Exception("Error on Delete", e);
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

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _entities;

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _entities;

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();

        }

        public PagedList<T> GetAllPaged(int pageNumber = 1, int pageSize = 10)
        {
            var all = _entities.AsQueryable();
            var count = all.Count();
            var items = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
