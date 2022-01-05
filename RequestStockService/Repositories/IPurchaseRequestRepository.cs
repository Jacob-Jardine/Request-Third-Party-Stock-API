using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;

namespace RequestStockService.Repositories
{
    public interface IPurchaseRequestRepository
    {
        public Task<bool> SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel);
        public Task<IEnumerable<ReadThirdPartyProductsDomainModel>> GetAllThirdPartyProducts();
    }
}
