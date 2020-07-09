using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.Tata.Buku.Shared.Infrastructures;
using DD.Tata.Buku.Shared.Logs;
using DD.TataBuku.Ledger.API.DataContext;
using DD.TataBuku.Ledger.API.Infrastructures;
using DD.TataBuku.Shared.Fault;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DD.TataBuku.Ledger.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IApiLogger, ApiLogger>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<GLDataContext>(op => op.UseNpgsql(Environment.GetEnvironmentVariable("GL_CONNECTION_STRING") ?? throw new InvalidOperationException(StaticMessage.INVALID_CONNECTION_STRING)));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationDecorator<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFailureMiddleware();
            app.MigrateDatabase();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
