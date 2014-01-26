using System;
using System.Collections.Generic;

namespace Bespoke.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRegistrationMethods UserRegistrationMethod { get; set; }
        public long? FacebookUserId { get; set; }
    }

    public enum UserRegistrationMethods : byte
    {
        Email = 1,
        Facebook = 2,
        Google = 3
    }
}
