using AutoMapper;
using Companys.Domain.Entities;
using Companys.Application.Models;
using Companys.Application.Features.CompanyCqrs.Commands.Create;
using Companys.Application.Features.CompanyCqrs.Commands.Delete;
using EventBus.Messages.Events;

namespace Companys.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyVm>().ReverseMap();
            CreateMap<Company, CreateCommand>().ReverseMap();
            CreateMap<Company, DeleteCommand>().ReverseMap();
            CreateMap<Company, StockDeleteEvent>().ReverseMap();
        }
    }
}
