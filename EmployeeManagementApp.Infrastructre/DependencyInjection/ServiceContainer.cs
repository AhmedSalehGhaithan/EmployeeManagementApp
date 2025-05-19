using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using EmployeeManagementApp.Infrastructre.Data;
using EmployeeManagementApp.Infrastructre.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementApp.Infrastructre.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructreService(
        this IServiceCollection services,
        IConfiguration _config)
    {
        services.AddScoped<IBaseRepository<Employee>, BaseRepository<ApplicationDbContext, Employee>>();
        services.AddScoped<IBaseRepository<Department>, BaseRepository<ApplicationDbContext, Department>>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
