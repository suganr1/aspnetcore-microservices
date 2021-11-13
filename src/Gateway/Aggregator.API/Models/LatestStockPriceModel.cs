using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Models
{
    public class LatestStockPriceModel : CompanyModel
    {
        public decimal Price { get; set; }
    }
}
