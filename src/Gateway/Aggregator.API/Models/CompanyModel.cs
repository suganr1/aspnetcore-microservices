using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Models
{
    public class CompanyModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Ceo { get; set; }

        public decimal TurnOver { get; set; }

        public string Website { get; set; }

        public int ExchangeTypeId { get; set; }
    }
}
