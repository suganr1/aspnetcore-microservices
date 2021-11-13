using Companys.Domain.Common;

namespace Companys.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Ceo { get; set; }

        public decimal TurnOver { get; set; }

        public string Website { get; set; }

        public int ExchangeTypeId { get; set; }

        public ExchangeType ExType { get; set; }
    }
}
