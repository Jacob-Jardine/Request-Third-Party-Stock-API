using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.Repositories
{
    /// <summary>
    /// Fake implementation for Third Party Stock service
    /// </summary>
    public class FakeThirdPartyStockRepository : IThirdPartyStockRepository
    {
        public List<PurchaseRequestSendDTO> _purchaseList;
        public List<ReadThirdPartyProductsDTO> _readProductList;
        /// <summary>
        /// Constructor instantiating fakes
        /// </summary>
        public FakeThirdPartyStockRepository()
        {
            _purchaseList = new List<PurchaseRequestSendDTO>(){};
            _readProductList = new List<ReadThirdPartyProductsDTO>() 
            {
                new ReadThirdPartyProductsDTO() {Id = 1, Ean = "11-22ee-ee23", CategoryId = 1, CategoryName = "Car", BrandId = 1, BrandName = "Fiat", Name = "Panda", Description = "Small Car", Price = 9999.99M, InStock = false}
            };
        }

        /// <summary>
        /// Returning all the products in the fake list
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ReadThirdPartyProductsDTO>> GetAllThirdPartyProducts() => Task.FromResult(_readProductList.AsEnumerable());

        /// <summary>
        /// Adding a purchase request to a fake list to simulate buying from the Third Part Stock service
        /// </summary>
        /// <param name="purchaseDomainModel"></param>
        /// <returns></returns>
        public async Task<bool> SendPurchaseRequest(PurchaseRequestSendDTO purchaseDomainModel)
        {
            try
            {
                _purchaseList.Add(purchaseDomainModel);
                return true;
            }
            catch
            {
                return false;
            }        
        }
    }
}
