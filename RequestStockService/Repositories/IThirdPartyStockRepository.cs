using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;

namespace RequestStockService.Repositories
{
    /// <summary>
    /// Interface for Third Party Stock service
    /// </summary>
    public interface IThirdPartyStockRepository
    {
        public Task<bool> SendPurchaseRequest(PurchaseRequestSendDTO purchaseDomainModel);
        public Task<IEnumerable<ReadThirdPartyProductsDTO>> GetAllThirdPartyProducts();
    }
}
