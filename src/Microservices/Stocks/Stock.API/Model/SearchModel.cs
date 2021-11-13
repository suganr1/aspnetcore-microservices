using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.API.Model
{
    public class SearchModel
    {
        public string Id { get; set; }
        public string CompanyCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
