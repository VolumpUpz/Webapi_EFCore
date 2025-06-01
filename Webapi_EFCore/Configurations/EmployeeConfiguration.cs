using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.FirstName).IsRequired(false).HasMaxLength(100);

            builder.Property(e => e.LastName).IsRequired(false).HasMaxLength(100);

            builder.Property(e => e.Salary);


            //One-to-one Employee <-> EmployeeDetails
            builder.HasOne(e => e.EmployeeDetails)
                .WithOne(e => e.Employee)
                .HasForeignKey<EmployeeDetails>(d => d.EmployeeId);

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.EmployeeId);

            builder.HasMany(e => e.Projects)
                .WithMany(e => e.Employees)
                .UsingEntity(j => j.ToTable("EmployeeProjects"));



        }
    }
}
