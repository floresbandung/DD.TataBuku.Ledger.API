using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.Tata.Buku.Shared.Infrastructures;
using DD.Tata.Buku.Shared.Logs;
using DD.TataBuku.Ledger.API.Context;
using DD.TataBuku.Ledger.API.DataContext;
using DD.TataBuku.Ledger.API.Infrastructures;
using DD.TataBuku.Shared.Fault;
using Hangfire;
using Hangfire.PostgreSql;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("PostgreSQL") ??
                throw new InvalidOperationException(StaticMessage.INVALID_CONNECTION_STRING);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IApiLogger, ApiLogger>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<GLDataContext>(Context);
            services.AddDbContext<ApplicationDbContext>(Context);
            services.AddHangfire(HangFireConfiguration);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationDecorator<,>));
        }

        public static string ConnectionString { get; private set; }

        private void Context(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(ConnectionString);
        }

        private void HangFireConfiguration(IGlobalConfiguration configuration)
        {
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(ConnectionString);
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

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
