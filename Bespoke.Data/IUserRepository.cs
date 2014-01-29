using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models;

namespace Bespoke.Data
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
