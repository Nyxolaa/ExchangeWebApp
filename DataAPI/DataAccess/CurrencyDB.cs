using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.DataAccess
{
    public class CurrencyDB:DbContext
    {
        public DbSet<CurrencyRecord>? currencyRecords { get; set; }

        public CurrencyDB(DbContextOptions<CurrencyDB> options):base(options)
        {

        }
                
    }
}
