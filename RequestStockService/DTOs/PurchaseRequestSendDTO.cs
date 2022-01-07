using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.DTOs
{
    /// <summary>
    /// DTO that takes the parameters needed to send a purchase request to
    /// the Third Party Stock service
    /// </summary>
    public class PurchaseRequestSendDTO
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
