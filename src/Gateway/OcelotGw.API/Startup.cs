using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGw.API
{
    public class Startup
    {
        private readonly string _policyName = "CorsPolicy";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.WithOrigins(Configuration["UIConfiguration:Uri"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();                    
                });
                //opt.AddPolicy(name: _anotherPolicy, builder =>
                //{
                //    builder.WithOrigins("https://localhost:5021")
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //});
            });

            services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(_policyName);

            //app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World! " + env.EnvironmentName);
                });
            });

            await app.UseOcelot();
        }
    }
}
