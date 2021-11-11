using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.DTOs
{
    public class PurchaseRequestReadDTO
    {
        public int PurchaseID { get; set; }
        public string PurchaseName { get; set; }
        public int PurchaseQTY { get; set; }
        public double PurchaseCost { get; set; }
    }
}
