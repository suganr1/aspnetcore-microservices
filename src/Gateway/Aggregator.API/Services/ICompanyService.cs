using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aggregator.API.Models;

namespace Aggregator.API.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetCompanyAll();
        Task<CompanyModel> GetCompanyByCode(string code);
    }
}
