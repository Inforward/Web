using System;
using System.Linq;
using System.Data.Entity;
using Bespoke.Models;

namespace Bespoke.Data.Sql.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public User GetByEmail(string email)
        {
            return GetDbSet<User>().FirstOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Create(User user)
        {
            GetDbSet<User>().Add(user);
            UnitOfWork.SaveChanges();
        }

        public void Update(User user)
        {
            SetEntityState(user, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }
    }
}