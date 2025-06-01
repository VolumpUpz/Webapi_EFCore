using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.ManagerId);

            builder.Property(e => e.FirstName).IsRequired(false).HasMaxLength(100);

            builder.Property(e => e.LastName).IsRequired(false).HasMaxLength(100);

            builder.HasMany(m => m.Employees)
                .WithOne(m => m.Manager)
                .HasForeignKey(m => m.ManagerId);
        }
    }
}
