using DD.TataBuku.Ledger.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DD.TataBuku.Ledger.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Startup.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GeneralLedger>(entity => { entity.HasIndex(e => e.GeneralLedgerNo).IsUnique(); });
        }

        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; }
        public virtual DbSet<GeneralLedgerDetail> GeneralLedgerDetails { get; set; }
    }
}
