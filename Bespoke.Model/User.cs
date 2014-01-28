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
        public string Name
        {
            get
            {
                var name = string.Empty;

                if (!string.IsNullOrEmpty(FirstName))
                    name += FirstName;

                if (!string.IsNullOrEmpty(LastName))
                {
                    if (name.Length > 0) name += " ";
                    name += LastName;
                }

                return name;
            }
        }
    }

    public enum UserRegistrationMethods : byte
    {
        Email = 1,
        Facebook = 2,
        Google = 3
    }
}
