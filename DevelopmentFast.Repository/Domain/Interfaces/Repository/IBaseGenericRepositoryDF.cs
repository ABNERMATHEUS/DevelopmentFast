using System.Linq.Expressions;

namespace DevelopmentFast.Repository.Domain.Interfaces.Repository
{
    public interface IBaseGenericRepositoryDF<TEntity, TPrimaryKey>
        where TEntity : class
        where TPrimaryKey : IComparable<TPrimaryKey>
    {
        #region CREATE
        Task<TEntity> CreateAsync(TEntity model);
        Task<IList<TEntity>> CreateRangeAsync(IList<TEntity> list);
        TEntity Create(TEntity model);
        IList<TEntity> CreateRange(IList<TEntity> list);
        #endregion

        #region UPDATE
        Task<TEntity> UpdateAsync(TEntity model);
        TEntity Update(TEntity model);
        Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> list);
        IList<TEntity> UpdateRange(IList<TEntity> list);
        #endregion

        #region DELETE
        Task<TEntity> DeleteAsync(TEntity model);
        TEntity Delete(TEntity model);
        #endregion

        #region READ 
        IQueryable<TEntity> GetById(TPrimaryKey primaryKey);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region SAVE
        void SaveChanges();
        Task SaveChangesAsync();
        #endregion


    }
}
