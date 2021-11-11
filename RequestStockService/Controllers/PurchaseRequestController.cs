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
    [Route("api/purchase")]
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

        [HttpGet("GetPurchaseRequests")]
        public async Task<ActionResult<IEnumerable<PurchaseRequestReadDTO>>> GetAllStaff()
        {
            try
            {
                var getAllStaff = await _purchaseRequestRepository.GetAllPurchaseAsync();
                return Ok(_mapper.Map<IEnumerable<PurchaseRequestReadDTO>>(getAllStaff));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("SendPurchaseRequest")]
        public async Task<ActionResult> SendPurchaseRequest([FromBody] PurchaseRequestSendDTO purchaseRequestDTO)
        {
            try 
            {
                var purchaseRequest = _mapper.Map<PurchaseRequestDomainModel>(purchaseRequestDTO);
                PurchaseRequestDomainModel newPurchaseRequestDomainModel = _purchaseRequestRepository.SendPurchaseRequest(purchaseRequest);
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
