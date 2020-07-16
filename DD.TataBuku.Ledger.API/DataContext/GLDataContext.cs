using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.TataBuku.Shared.Fault;
using Microsoft.EntityFrameworkCore;

namespace DD.TataBuku.Ledger.API.DataContext
{
    public class GLDataContext : DbContext
    {
        public GLDataContext(DbContextOptions<GLDataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Startup.ConnectionString);
            }
        }
    }
}
