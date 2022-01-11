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
    /// <summary>
    /// Concrete implementation for interacting with the Third Party Stock service
    /// </summary>
    public class ThirdPartyStockRepository : IThirdPartyStockRepository
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        /// <summary>
        /// Constructor instantiating configuring the HttpClient
        /// </summary>
        /// <param name="config"></param>
        /// <param name="client"></param>
        public ThirdPartyStockRepository(IConfiguration config, HttpClient client)
        {
            _config = config;
            string baseUrl = config["THIRD_PARTY_BASE_URL"];
            client.BaseAddress = new System.Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        /// <summary>
        /// Sends a post request to the Third Party Stock service to buy stock
        /// </summary>
        /// <param name="sendPurchaseRequestDTO"></param>
        /// <returns></returns>
        public async Task<bool> SendPurchaseRequest(PurchaseRequestSendDTO sendPurchaseRequestDTO)
        {
            if(sendPurchaseRequestDTO == null) 
            {
                return false;
            }
            var json = JsonSerializer.Serialize(sendPurchaseRequestDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Order", data);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        /// <summary>
        /// Sends a get request to the Third Party Stock service to get a list of all products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ReadThirdPartyProductsDTO>> GetAllThirdPartyProducts()
        {
            var response = await _client.GetAsync("Product");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsAsync<IEnumerable<ReadThirdPartyProductsDTO>>();
            return products;
        }
    }
}
