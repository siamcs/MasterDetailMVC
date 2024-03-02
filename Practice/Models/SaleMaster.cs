using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Practice.VM.VmSale;

namespace Practice.Models
{
    public class SaleMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        [Required]
        public string CustomeName { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }
        [Column("date")]
        public DateTime? CreateDate { get; set; }
        
        public List<SaleDetail> SaleDetails { get; set; }
    }
}