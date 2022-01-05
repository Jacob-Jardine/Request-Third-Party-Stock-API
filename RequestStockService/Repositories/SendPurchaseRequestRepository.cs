using Microsoft.Extensions.Configuration;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace RequestStockService.Repositories
{
    public class SendPurchaseRequestRepository : IPurchaseRequestRepository
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public SendPurchaseRequestRepository(IConfiguration config, HttpClient client)
        {
            _config = config;
            client.BaseAddress = _config.GetValue<Uri>("THIRD_PARTY_BASE_URL");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public async Task<bool> SendPurchaseRequest(PurchaseRequestDomainModel purchaseDomainModel)
        {
            var json = JsonSerializer.Serialize(purchaseDomainModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Order", data);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<IEnumerable<ReadThirdPartyProductsDomainModel>> GetAllThirdPartyProducts()
        {
            var response = await _client.GetAsync("Product");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsAsync<IEnumerable<ReadThirdPartyProductsDomainModel>>();
            return products;
        }
    }
}
