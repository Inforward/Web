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

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository<User>.Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IList<User> Find(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}