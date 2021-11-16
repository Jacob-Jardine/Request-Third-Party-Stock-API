using AutoMapper;
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

        [HttpPost("Send-Purchase-Request")]
        public async Task<ActionResult> SendPurchaseRequest([FromBody] PurchaseRequestSendDTO purchaseRequestDTO)
        {
            try 
            {
                var purchaseRequest = _mapper.Map<PurchaseRequestDomainModel>(purchaseRequestDTO);
                await _purchaseRequestRepository.SendPurchaseRequest(purchaseRequest);
                await _purchaseRequestRepository.SaveChangesAsync();
                return Ok();
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
