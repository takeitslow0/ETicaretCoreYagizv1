using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.EntityFramework.Bases
{
    public interface IRepoBase<TEntity, TDbContext> : IDisposable where TEntity : class, new() where TDbContext : DbContext, new()
    {
        // (C)reate(R)ead(U)pdate(D)elete crud
        TDbContext DbContext { get; set; }
        IQueryable<TEntity> Query(params string[] entitiesToInclude); // read
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude);
        void Add(TEntity entity, bool save = true); // create
        void Update(TEntity entity, bool save = true); // update
        void Delete(TEntity entity, bool save = true); // delete
        void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true);
        int Save();
    }
}
