using AppCore.DataAccess.EntityFramework.Bases;
using Microsoft.EntityFrameworkCore;

namespace AppCore.DataAccess.EntityFramework
{
    public class Repo<TEntity, TDbContext> : RepoBase<TEntity, TDbContext> where TEntity : class, new() where TDbContext : DbContext, new()
    {
        public Repo() : base()
        {

        }

        public Repo(TDbContext dbContxt) : base(dbContxt)
        {

        }
    }
}