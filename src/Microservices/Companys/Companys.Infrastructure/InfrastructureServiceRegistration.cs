using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Companys.Application.Persistence;
using Companys.Application.Models;
using Companys.Infrastructure.Persistence;
using Companys.Infrastructure.Repositories;

namespace Companys.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}
