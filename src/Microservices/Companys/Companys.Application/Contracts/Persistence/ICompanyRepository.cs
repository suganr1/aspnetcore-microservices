using System.Collections.Generic;
using System.Threading.Tasks;
using Companys.Domain.Entities;

namespace Companys.Application.Persistence
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
        Task<IEnumerable<Company>> GetCompanyAll();

        Task<Company> GetCompanyByCode(string code);
    }
}
