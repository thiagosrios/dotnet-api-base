using ApiBase.Database;
using ApiBase.Filters;
using ApiBase.Interfaces;
using ApiBase.Repositories;
using ApiBase.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace ApiBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddControllers();
            services
                .AddMvc(options =>
                {
                    // Inclui filtros para exce��es e erros de valida��o
                    options.Filters.Add(typeof(ValidateModelFilter));
                    options.Filters.Add(typeof(ExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    // Op��es de serializa��o JSON, ignorando nulos
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("TrustedHosts", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Headers.Append("Content-Type", "application/json; charset=utf-8");
                    await context.Response.WriteAsync("API em Execu��o");
                });
            });
        }
    }
}
