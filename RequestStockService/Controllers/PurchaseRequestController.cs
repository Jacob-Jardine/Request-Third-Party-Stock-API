using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using RequestStockService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.Controllers
{
    [Route("api/third-party")]
    [ApiController]
    public class PurchaseRequestController : ControllerBase
    {
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;
        private IMapper _mapper;

        public PurchaseRequestController(IPurchaseRequestRepository purchaseRequestRepository, IMapper mapper)
        {
            _purchaseRequestRepository = purchaseRequestRepository;
            _mapper = mapper;
        }

        [HttpGet("Get-Third-Party-Products")]
        [Authorize("ReadThirdPartyStock")]
        public async Task<ActionResult<IEnumerable<ReadThirdPartyProductsDTO>>> GetThirdPartyProducts()
        {
            try
            {
                var products = await _purchaseRequestRepository.GetAllThirdPartyProducts();
                return Ok(_mapper.Map<ReadThirdPartyProductsDTO>(products));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Send-Purchase-Request")]
        [Authorize("SendThirdPartyRequest")]
        public async Task<ActionResult> SendPurchaseRequest([FromBody] PurchaseRequestSendDTO purchaseRequestDTO)
        {
            try 
            {
                var purchaseRequest = _mapper.Map<PurchaseRequestDomainModel>(purchaseRequestDTO);
                await _purchaseRequestRepository.SendPurchaseRequest(purchaseRequest);
                return Ok("Purchase Request Has Been Accepeted By The Third Party Provider");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
