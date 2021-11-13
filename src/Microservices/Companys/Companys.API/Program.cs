using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Common.Logging;
using Companys.API.Extensions;
using Companys.Infrastructure.Persistence;

namespace Companys.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<CompanyContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<CompanyContextSeed>>();
                    CompanyContextSeed
                        .SeedAsync(context, logger)
                        .Wait();
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
