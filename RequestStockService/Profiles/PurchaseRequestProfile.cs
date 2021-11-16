using AutoMapper;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.Profiles
{
    public class PurchaseRequestProfile : Profile
    {
        public PurchaseRequestProfile() 
        {
            CreateMap<PurchaseRequestDomainModel, PurchaseRequestSendDTO>();
            CreateMap<PurchaseRequestSendDTO, PurchaseRequestDomainModel>();
            CreateMap<PurchaseRequestDomainModel, PurchaseRequestReadDTO>();
            CreateMap<PurchaseRequestReadDTO, PurchaseRequestDomainModel>();
            CreateMap<ReadThirdPartyProductsDomainModel, ReadThirdPartyProductsDTO>();
            CreateMap<ReadThirdPartyProductsDTO, ReadThirdPartyProductsDomainModel>();
        }
    }
}
