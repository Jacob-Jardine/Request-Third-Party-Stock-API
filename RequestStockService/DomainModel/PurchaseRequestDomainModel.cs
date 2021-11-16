using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.DomainModel
{
    public class PurchaseRequestDomainModel
    {
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
