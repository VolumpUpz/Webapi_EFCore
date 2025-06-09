using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            builder.Property(u => u.IsDeleted)
                .HasColumnName("is_deleted");
        }
    }
}
