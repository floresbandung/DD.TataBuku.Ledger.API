using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.TataBuku.Ledger.API.Context;
using DD.TataBuku.Ledger.API.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DD.TataBuku.Ledger.API.Infrastructures
{
    public static class Extensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<GLDataContext>();
            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                context.Database.Migrate();
            
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

            return builder;
        }
    }
}
