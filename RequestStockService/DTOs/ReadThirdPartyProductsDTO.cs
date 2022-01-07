using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.DTOs
{
    /// <summary>
    /// DTO that takes the parameters needed to read a product from 
    /// the Third Party Stock service
    /// </summary>
    public class ReadThirdPartyProductsDTO
    {
        public int Id { get; set; }
        public string Ean { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
}
