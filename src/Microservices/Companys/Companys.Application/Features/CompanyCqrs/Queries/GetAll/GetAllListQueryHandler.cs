using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Companys.Application.Persistence;
using Companys.Application.Models;

namespace Companys.Application.Features.CompanyCqrs.Queries.GetAll
{
    public class GetAllListQueryHandler : IRequestHandler<GetAllListQuery, List<CompanyVm>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllListQueryHandler(ICompanyRepository orderRepository, IMapper mapper)
        {
            _companyRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<CompanyVm>> Handle(GetAllListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _companyRepository.GetCompanyAll();
            return _mapper.Map<List<CompanyVm>>(orderList);
        }
    }
}
