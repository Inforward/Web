using System.Data.Entity;
using Bespoke.Data.Sql.Mapping;
using Bespoke.Models;

namespace Bespoke.Data.Sql.Models
{
    public class BespokeContext : DbContext, IUnitOfWork
    {
        static BespokeContext()
        {
            Database.SetInitializer<BespokeContext>(null);
        }

        public BespokeContext()
            : base("Name=BespokeContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }

        void IUnitOfWork.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
