using System.Data.Entity.ModelConfiguration;
using Bespoke.Models;

namespace Bespoke.Data.Sql.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.UserId);

            // Properties
            Property(t => t.Email).IsRequired().HasMaxLength(100);
            Property(t => t.PasswordHash).HasMaxLength(256);
            Property(t => t.PasswordSalt).HasMaxLength(256);

            Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            Property(t => t.LastName).IsRequired().HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Users");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.FacebookUserId).HasColumnName("FacebookUserId");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.UserRegistrationMethod).HasColumnName("UserRegistrationMethodId");
        }
    }
}