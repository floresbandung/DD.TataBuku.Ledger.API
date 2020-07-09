using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.TataBuku.Ledger.API.Models
{
    public class GeneralLedger : BaseModel
    {
        public GeneralLedger()
        {
            GeneralLedgerDetails = new HashSet<GeneralLedgerDetail>();
        }

        public ICollection<GeneralLedgerDetail> GeneralLedgerDetails { get; set; }

        [Required]
        [StringLength(24)]
        public string GeneralLedgerNo { get; set; }

        [Required]
        [StringLength(128)]
        public string GeneralLedgerName { get; set; }

        [Required]
        public DateTime GeneralLedgerDate { get; set; }

        [Required]
        [StringLength(24)]
        public string RefDocumentNo { get; set; }

        public DateTime? PostingDate { get; set; }
    }
}
