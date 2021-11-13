using MediatR;
using System.Collections.Generic;
using Companys.Application.Models;

namespace Companys.Application.Features.CompanyCqrs.Queries.GetAll
{
    public class GetAllListQuery : IRequest<List<CompanyVm>>
    {
        public GetAllListQuery()
        {
            //
        }
    }
}
