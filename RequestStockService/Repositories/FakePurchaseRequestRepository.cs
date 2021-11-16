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
        public FakePurchaseRequestRepository()
        {
            _purchaseList = new List<PurchaseRequestDomainModel>(){};
        }

        public Task SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel)
        {
            _purchaseList.Add(purchaseDomainModel);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ReadThirdPartyProductsDomainModel>> GetAllThirdPartyProducts()
        {
            throw new NotImplementedException();
        }
    }
}
