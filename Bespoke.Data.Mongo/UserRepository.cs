using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models;

namespace Bespoke.Data.Mongo
{
    public class UserRepository : MongoDbRepository<User>, IUserRepository
    {
        public User GetByEmail(string email)
        {
            return Find(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}
