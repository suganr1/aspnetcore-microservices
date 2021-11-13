using System;
using MediatR;
using Companys.Application.Models;

namespace Companys.Application.Features.CompanyCqrs.Queries.GetByCode
{
    public class GetByCodeListQuery : IRequest<CompanyVm>
    {
        public string CompanyCode { get; set; }

        public GetByCodeListQuery(string companyCode)
        {
            CompanyCode = companyCode ?? throw new ArgumentNullException(nameof(companyCode));
        }
    }
}
