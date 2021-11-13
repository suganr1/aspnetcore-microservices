using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Models
{
    public class StockModel
    {
        public string Id { get; set; }
        public string CompanyCode { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
