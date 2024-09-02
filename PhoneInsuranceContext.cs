csharp
using System.Data.Entity;

namespace PhoneInsurance.Models
{
    public class PhoneInsuranceContext : DbContext
    {
        public DbSet<PhoneInsuranceQuote> PhoneInsuranceQuotes { get; set; }
    }
}
