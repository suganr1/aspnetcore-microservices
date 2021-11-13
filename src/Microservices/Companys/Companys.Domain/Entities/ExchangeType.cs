using System.Collections.Generic;
using Companys.Domain.Common;

namespace Companys.Domain.Entities
{
    public class ExchangeType : BaseEntity
    {
        public string StockExchange { get; set; }

        public ICollection<Company> Comp { get; set; }
    }
}
