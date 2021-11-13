
namespace Companys.Application.Models
{
    public class CompanyVm
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Ceo { get; set; }

        public decimal TurnOver { get; set; }

        public string Website { get; set; }

        public int ExchangeTypeId { get; set; }
    }
}
