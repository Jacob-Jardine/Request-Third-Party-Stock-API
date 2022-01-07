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
    public class FakePurchaseRequestRepository : IPurchaseRequestRepository
    {
        private readonly List<PurchaseRequestSendDTO> _purchaseList;
        private readonly List<ReadThirdPartyProductsDTO> _readProductList;
        /// <summary>
        /// Constructor instantiating fakes
        /// </summary>
        public FakePurchaseRequestRepository()
        {
            _purchaseList = new List<PurchaseRequestSendDTO>(){};
            _readProductList = new List<ReadThirdPartyProductsDTO>() 
            {
                new ReadThirdPartyProductsDTO() {BrandId = 1}
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
