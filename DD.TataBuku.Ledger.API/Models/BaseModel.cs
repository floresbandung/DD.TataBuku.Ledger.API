using System;
using System.ComponentModel.DataAnnotations;

namespace DD.TataBuku.Ledger.API.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public byte RowStatus { get; set; }

        //[Required]
        //public byte[] RowVersion { get; set; }

        [Required]
        [StringLength(24)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(24)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


    }
}
