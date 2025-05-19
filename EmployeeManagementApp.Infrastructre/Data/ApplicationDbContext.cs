using EmployeeManagementApp.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Infrastructre.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)  {  }

    public DbSet<Employee> employees => Set<Employee>();
    public DbSet<Department> department => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .HasOne(o => o.Department)
            .WithMany(o => o.Employees)
            .HasForeignKey(o => o.departmentId)
            .IsRequired(false);

        modelBuilder.Entity<Department>()
            .HasMany(o => o.Employees)
            .WithOne(o => o.Department)
            .HasForeignKey(o => o.departmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
