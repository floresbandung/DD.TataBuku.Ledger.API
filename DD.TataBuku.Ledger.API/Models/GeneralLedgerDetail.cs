using System;
using System.ComponentModel.DataAnnotations;

namespace DD.TataBuku.Ledger.API.Models
{
    public class GeneralLedgerDetail : BaseModel
    {
        public virtual GeneralLedger GeneralLedger { get; set; }

        [Required]
        public Guid GeneralLedgerId { get; set; }

        [Required]
        [StringLength(24)]
        public string AccountCode { get; set; }

        [Required]
        [StringLength(256)]
        public string AccountName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        [StringLength(24)]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }

        [Required]
        public decimal ExchangeAmount { get; set; }

    }
}
