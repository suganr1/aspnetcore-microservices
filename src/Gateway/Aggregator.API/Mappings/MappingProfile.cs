using Aggregator.API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LatestStockPriceModel, CompanyModel>().ReverseMap();
        }
    }
}
