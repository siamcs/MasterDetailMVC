using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practice.Models
{
    [Table("SaleDetail")]
    public class SaleDetail
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleDetailId { get; set; }
        [ForeignKey("SaleMaster")]
        public int SaleId { get; set; }
      
        public string ProductName { get; set; }
       
        public int Price { get; set; }
        public virtual SaleMaster SaleMaster { get; set; }
    }
}