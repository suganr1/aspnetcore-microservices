using MediatR;

namespace Companys.Application.Features.CompanyCqrs.Commands.Create
{
    public class CreateCommand : IRequest<int>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string CEO { get; set; }

        public decimal TurnOver { get; set; }

        public string Website { get; set; }

        public int ExchangeTypeId { get; set; }
    }
}
