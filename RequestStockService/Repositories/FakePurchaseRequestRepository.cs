using RequestStockService.DomainModel;
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

        public PurchaseRequestDomainModel SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel)
        {
            _purchaseList.Add(purchaseDomainModel);
            return purchaseDomainModel;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
