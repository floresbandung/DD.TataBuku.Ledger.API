using DD.TataBuku.Ledger.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DD.TataBuku.Ledger.API.Context
{
    public class AccountingEntities : DbContext
    {
        public AccountingEntities(DbContextOptions<AccountingEntities> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GeneralLedger>(entity => { entity.HasIndex(e => e.GeneralLedgerNo).IsUnique(); });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=MRP_ACCOUNTING;User Id=postgres;Password=getdown;");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; }
        public virtual DbSet<GeneralLedgerDetail> GeneralLedgerDetails { get; set; }
    }
}
