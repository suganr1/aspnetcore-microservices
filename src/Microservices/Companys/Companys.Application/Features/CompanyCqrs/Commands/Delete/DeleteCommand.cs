using MediatR;

namespace Companys.Application.Features.CompanyCqrs.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public string CompanyCode { get; set; }
    }
}
