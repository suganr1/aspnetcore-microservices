using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Companys.Domain.Entities;
using Companys.Infrastructure.Persistence;
using Companys.Application.Persistence;
using System.Linq;

namespace Companys.Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Company>> GetCompanyAll()
        {
            var allCompany = await _dbContext.Company.OrderByDescending(o => o.CreatedDate).ThenByDescending(o => o.Code).ToListAsync();
            return allCompany;
        }

        public async Task<Company> GetCompanyByCode(string code)
        {
            var company = await _dbContext.Company.FirstOrDefaultAsync(c => c.Code == code);
            return company;
        }
    }
}
