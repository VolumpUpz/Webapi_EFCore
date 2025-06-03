using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {

            builder.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(e => e.EmployeeId);

            builder.HasOne(p => p.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(p => p.ProjectId);
        }
    }
}
