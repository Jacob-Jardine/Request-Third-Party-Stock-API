using AutoMapper;
using RequestStockService.DomainModel;
using RequestStockService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestStockService.Profiles
{
    /// <summary>
    /// Mapper class that quickly and easily converts one model to another
    /// </summary>
    public class PurchaseRequestProfile : Profile
    {
        public PurchaseRequestProfile() 
        {
            CreateMap<PurchaseRequestDomainModel, PurchaseRequestSendDTO>();
            CreateMap<PurchaseRequestSendDTO, PurchaseRequestDomainModel>();
            CreateMap<ReadThirdPartyProductsDomainModel, ReadThirdPartyProductsDTO>();
            CreateMap<ReadThirdPartyProductsDTO, ReadThirdPartyProductsDomainModel>();
        }
    }
}
