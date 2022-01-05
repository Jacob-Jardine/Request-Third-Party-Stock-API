using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.Repositories
{
    public class FakePurchaseRequestRepository : IPurchaseRequestRepository
    {
        private readonly List<PurchaseRequestDomainModel> _purchaseList;
        private readonly List<ReadThirdPartyProductsDomainModel> _readProductList;
        public FakePurchaseRequestRepository()
        {
            _purchaseList = new List<PurchaseRequestDomainModel>(){};
            _readProductList = new List<ReadThirdPartyProductsDomainModel>() 
            {
                new ReadThirdPartyProductsDomainModel() {Id = 1, Ean = "5 102310 300410", CategoryId = 1, CategoryName = "Screen Protectors", BrandId = 1, BrandName = "iStuff-R-Us", Name = "Rippled Screen Protector", Description = "For his or her sensory pleasure. Fits few known smartphones.", Price = 6.03m, InStock = true}
            };
        }

        public Task<IEnumerable<ReadThirdPartyProductsDomainModel>> GetAllThirdPartyProducts() => Task.FromResult(_readProductList.AsEnumerable());

        public async Task<bool> SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel)
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
