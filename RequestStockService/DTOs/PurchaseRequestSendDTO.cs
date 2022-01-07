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
        public string AccountName { get; set; }
        public string CardNumber { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
