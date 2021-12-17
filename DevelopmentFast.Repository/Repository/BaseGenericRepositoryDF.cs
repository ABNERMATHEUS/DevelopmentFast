using DevelopmentFast.Repository.Domain.Entity;
using DevelopmentFast.Repository.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevelopmentFast.Repository.Repository
{
    public abstract class BaseGenericRepositoryDF<TEntity, TPrimaryKey> : IBaseGenericRepositoryDF<TEntity, TPrimaryKey>
        where TEntity : class
        where TPrimaryKey : IComparable<TPrimaryKey>
    {
        protected readonly DbContext _dbContext;

        protected BaseGenericRepositoryDF(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region CREATE
        public virtual TEntity Create(TEntity model)
        {
            _dbContext.Set<TEntity>().Add(model);
            AddTimestamps(model);
            SaveChanges();
            return model;
        }

        public async virtual Task<TEntity> CreateAsync(TEntity model)
        {
            await _dbContext.Set<TEntity>().AddAsync(model);
            AddTimestamps(model);
            await SaveChangesAsync();
            return model;
        }

        public async Task<IList<TEntity>> CreateRangeAsync(IList<TEntity> list)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(list);
            AddTimestamps(list);
            await SaveChangesAsync();
            return list;
        }

        public IList<TEntity> CreateRange(IList<TEntity> list)
        {
            _dbContext.Set<TEntity>().AddRange(list);
            AddTimestamps(list);
            SaveChanges();
            return list;
        }

        #endregion

        #region READ
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression);
        }


        public virtual IQueryable<TEntity> GetById(TPrimaryKey primaryKey)
        {
            return _dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution().Where(x => (x as BaseEntityDF<TPrimaryKey>).Id.Equals(primaryKey));
        }



        #endregion

        #region UPDATE
        public virtual TEntity Update(TEntity model)
        {
            _dbContext.Set<TEntity>().Update(model);
            AddTimestamps(model);
            SaveChanges();
            return model;
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity model)
        {

            _dbContext.Set<TEntity>().Update(model);
            AddTimestamps(model);
            await SaveChangesAsync();
            return model;
        }

        public async virtual Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> list)
        {

            _dbContext.Set<TEntity>().UpdateRange(list);
            AddTimestamps(list);
            await SaveChangesAsync();
            return list;
        }

        public virtual IList<TEntity> UpdateRange(IList<TEntity> list)
        {
            _dbContext.Set<TEntity>().UpdateRange(list);
            AddTimestamps(list);
            SaveChanges();
            return list;
        }
        #endregion

        #region DELETE
        public virtual TEntity Delete(TEntity model)
        {
            _dbContext.Remove(model);
            SaveChanges();
            return model;
        }

        public async virtual Task<TEntity> DeleteAsync(TEntity model)
        {
            _dbContext.Remove(model);
            await SaveChangesAsync();
            return model;
        }

        #endregion

        #region SAVE
        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async virtual Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        private void AddTimestamps(TEntity model)
        {
            var entities = _dbContext.ChangeTracker
                            .Entries()
                            .Where(x => x.Entity is BaseEntityDF<TPrimaryKey> && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).CreatedAt = now;
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).UpdatedAt = null;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).UpdatedAt = now;
                    _dbContext.Entry(model).Property("CreatedAt").IsModified = false;
                }
            }


        }

        private void AddTimestamps(IList<TEntity> list)
        {
            var entities = _dbContext.ChangeTracker
                            .Entries()
                            .Where(x => x.Entity is BaseEntityDF<TPrimaryKey> && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).CreatedAt = now;
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).UpdatedAt = null;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((BaseEntityDF<TPrimaryKey>)entity.Entity).UpdatedAt = now;
                    list.ToList().ForEach(x =>
                    {
                        _dbContext.Entry(x).Property("CreatedAt").IsModified = false;
                    });

                }
            }


        }

    }
}
