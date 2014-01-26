using System;
using System.Data.Entity;
using Bespoke.Data.Sql.Models;

namespace Bespoke.Data.Sql.Repositories
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected BespokeContext Context
        {
            get { return (BespokeContext)UnitOfWork; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }
    }
}
