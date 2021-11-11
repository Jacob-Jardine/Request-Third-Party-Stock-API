using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestStockService.DomainModel;

namespace RequestStockService.Repositories
{
    public interface IPurchaseRequestRepository
    {
        public Task<IEnumerable<PurchaseRequestDomainModel>> GetAllPurchaseAsync();
        public PurchaseRequestDomainModel SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel);
        public Task SaveChangesAsync();
    }
}
