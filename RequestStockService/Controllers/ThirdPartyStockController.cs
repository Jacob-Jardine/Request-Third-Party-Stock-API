using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RequestStockService.DTOs;
using RequestStockService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestStockService.Controllers
{
    /// <summary>
    /// Controller for the Third Party Stock service
    /// </summary>
    [Route("api/third-party")]
    [ApiController]
    public class ThirdPartyStockController : ControllerBase
    {
        private readonly IThirdPartyStockRepository _purchaseRequestRepository;
        private readonly ILogger<ThirdPartyStockController> _logger;
        private IMapper _mapper;

        /// <summary>
        /// Constructor that points towards the correct implementation of the Third Party Stock service
        /// based on dependecy injection
        /// </summary>
        /// <param name="purchaseRequestRepository"></param>
        /// <param name="mapper"></param>
        public ThirdPartyStockController(IThirdPartyStockRepository purchaseRequestRepository, IMapper mapper, ILogger<ThirdPartyStockController> logger)
        {
            _purchaseRequestRepository = purchaseRequestRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Controller action that sends a request to the Third Party Stock service and returns a list
        /// of all the products that it has
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Third-Party-Products")]
        [Authorize("ReadThirdPartyStock")]
        public async Task<ActionResult<IEnumerable<ReadThirdPartyProductsDTO>>> GetThirdPartyProducts()
        {
            try
            {
                var products = await _purchaseRequestRepository.GetAllThirdPartyProducts();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Controller action that takes a PurchaseRequestSendDTO from an incomming post request and then 
        /// sends that to the Third Party Stock service
        /// </summary>
        /// <param name="purchaseRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("Send-Purchase-Request")]
        [Authorize("SendThirdPartyRequest")]
        public async Task<ActionResult> SendPurchaseRequest([FromBody] PurchaseRequestSendDTO purchaseRequestDTO)
        {
            try 
            {
                if (purchaseRequestDTO == null)
                {
                    return BadRequest("Purchase Request Can't Be Null");
                }
                await _purchaseRequestRepository.SendPurchaseRequest(purchaseRequestDTO);
                return Ok("Purchase Request Has Been Accepeted By The Third Party Provider");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}