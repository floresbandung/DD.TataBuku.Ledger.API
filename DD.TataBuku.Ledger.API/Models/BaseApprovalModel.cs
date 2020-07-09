using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.TataBuku.Ledger.API.Models
{
    public class BaseApprovalModel
    {
        [StringLength(24)]
        public string StatusApproval { get; set; }

        [Required]
        [StringLength(24)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(64)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(64)]
        public string DivisionName { get; set; }

        [Required]
        [StringLength(64)]
        public string Position { get; set; }

        [Required]
        [StringLength(64)]
        public string Location { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string CcEmail { get; set; }
    }
}
