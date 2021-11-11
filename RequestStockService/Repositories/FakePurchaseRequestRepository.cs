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
            _purchaseList = new List<PurchaseRequestDomainModel>()
            {
                new PurchaseRequestDomainModel() { PurchaseID = 1, PurchaseName = "test", PurchaseQTY = 10, PurchaseCost = 111 }
            };
        }

        public Task<IEnumerable<PurchaseRequestDomainModel>> GetAllPurchaseAsync() => Task.FromResult(_purchaseList.AsEnumerable());

        public PurchaseRequestDomainModel SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel)
        {
            int newPurchaseRequestID = 2;
            purchaseDomainModel.PurchaseID = newPurchaseRequestID;
            _purchaseList.Add(purchaseDomainModel);
            return purchaseDomainModel;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
