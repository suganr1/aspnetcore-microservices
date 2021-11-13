using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Companys.Application.Persistence;
using Companys.Application.Models;

namespace Companys.Application.Features.CompanyCqrs.Queries.GetByCode
{
    public class GetByCodeListQueryHandler : IRequestHandler<GetByCodeListQuery, CompanyVm>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetByCodeListQueryHandler(ICompanyRepository orderRepository, IMapper mapper)
        {
            _companyRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompanyVm> Handle(GetByCodeListQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByCode(request.CompanyCode);
            return _mapper.Map<CompanyVm>(company);
        }
    }
}
