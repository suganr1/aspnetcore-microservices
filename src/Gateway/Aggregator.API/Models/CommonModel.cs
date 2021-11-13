using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Models
{
    public class CommonModel
    {
        public string CompanyCode { get; set; }
        public CompanyModel CompanyDetails { get; set; }
        public IEnumerable<StockModel> StockDetails { get; set; }
    }
}
