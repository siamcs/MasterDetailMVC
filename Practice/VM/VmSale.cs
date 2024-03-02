using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practice.VM
{
    public class VmSale
    {
        public int SaleId { get; set; }
        [Required(ErrorMessage = "Please Enter Name"), StringLength(40)]
        public string CustomeName { get; set;}
        public string Gender { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }
        public DateTime? CreateDate { get; set; }
        public string[] ProductName { get; set; }
        public int[] Price { get; set; }
        public List<VmSaleDetail> SaleDetails { get; set; } = new List<VmSaleDetail>();
        public class VmSaleDetail
        {
            public int SaleDetailId { get; set; }
            public int SaleId { get; set; }
            public string ProductName { get; set; }
            public int Price { get; set; }
        }

    }
}