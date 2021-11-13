using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Companys.Domain.Entities;

namespace Companys.Infrastructure.Persistence
{
    public class CompanyContextSeed
    {
        public static async Task SeedAsync(CompanyContext companyContext, ILogger<CompanyContextSeed> logger)
        {
            if (!companyContext.ExchangeType.Any())
            {
                companyContext.ExchangeType.AddRange(GetDefaultExchangeType());
                await companyContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(CompanyContext).Name);
            }

            if (!companyContext.Company.Any())
            {
                companyContext.Company.AddRange(GetDefaultCompany());
                await companyContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(CompanyContext).Name);
            }
        }

        private static IEnumerable<ExchangeType> GetDefaultExchangeType()
        {
            return new List<ExchangeType>
            {
                new ExchangeType() {StockExchange = "BSE" },
                new ExchangeType() {StockExchange = "NSE" }
            };
        }
        private static IEnumerable<Company> GetDefaultCompany()
        {
            return new List<Company>
            {
                new Company() {Code = "A001", Name = "ANGEL", Ceo = "SUGAN", TurnOver = 100000000, Website = "WWW.ANGEL.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A002", Name = "BOBBY", Ceo = "SUGAN", TurnOver = 200000000, Website = "WWW.BOBBY.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A003", Name = "ALICE", Ceo = "SUGAN", TurnOver = 300000000, Website = "WWW.ALICE.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A004", Name = "ARIEL", Ceo = "SUGAN", TurnOver = 400000000, Website = "WWW.ARIEL.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A005", Name = "CARLY", Ceo = "SUGAN", TurnOver = 500000000, Website = "WWW.CARLY.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A006", Name = "CHLOE", Ceo = "SUGAN", TurnOver = 600000000, Website = "WWW.CHLOE.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A007", Name = "EDITH", Ceo = "SUGAN", TurnOver = 700000000, Website = "WWW.EDITH.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A008", Name = "FIONA", Ceo = "SUGAN", TurnOver = 800000000, Website = "WWW.FIONA.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A009", Name = "FLORA", Ceo = "SUGAN", TurnOver = 900000000, Website = "WWW.FLORA.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A010", Name = "GRETA", Ceo = "SUGAN", TurnOver = 1000000000,Website = "WWW.GRETA.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A011", Name = "HAZEL", Ceo = "SUGAN", TurnOver = 1100000000,Website = "WWW.HAZEL.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A012", Name = "HEIDI", Ceo = "SUGAN", TurnOver = 1200000000,Website = "WWW.HEIDI.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A013", Name = "IRENE", Ceo = "SUGAN", TurnOver = 1300000000,Website = "WWW.IRENE.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A014", Name = "JENNA", Ceo = "SUGAN", TurnOver = 1400000000,Website = "WWW.JENNA.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A015", Name = "JOYCE", Ceo = "SUGAN", TurnOver = 1500000000,Website = "WWW.JOYCE.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A016", Name = "KEIRA", Ceo = "SUGAN", TurnOver = 1600000000,Website = "WWW.KEIRA.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A017", Name = "MACIE", Ceo = "SUGAN", TurnOver = 1700000000,Website = "WWW.MACIE.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A018", Name = "TIANA", Ceo = "SUGAN", TurnOver = 1800000000,Website = "WWW.TIANA.COM", ExchangeTypeId = 2 },
                new Company() {Code = "A019", Name = "WILLA", Ceo = "SUGAN", TurnOver = 1900000000,Website = "WWW.WILLA.COM", ExchangeTypeId = 1 },
                new Company() {Code = "A020", Name = "ZAHRA", Ceo = "SUGAN", TurnOver = 2000000000,Website = "WWW.ZAHRA.COM", ExchangeTypeId = 2 }
            };
        }
    }
}
