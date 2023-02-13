using BussinessAPI.Cache;
using DataAPI.DataAccess;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BussinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;
        private readonly CurrencyDB _context;
        public CacheController(CurrencyDB context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string currCode)
        {
            ICollection<CurrencyRecord> records = _cacheService.GetData<ICollection<CurrencyRecord>>(currCode);


            if (records == null || records.Count == 0)
            {
                records = _context.currencyRecords.Where(x=>x.CurrencyCode==currCode).ToList();

                _cacheService.SetData<ICollection<CurrencyRecord>>(currCode, records, DateTimeOffset.Now.AddDays(1));
            }

            return Ok(records);
        }

        [HttpGet("currencyCodes")]
        public IActionResult GetCurrenyCode()
        {

            var codes = _cacheService.GetData<ICollection<string>>("currencyCodes");

            if (codes == null)
            {
                codes =  _context.currencyRecords.Select(m => m.CurrencyCode).Distinct().ToList();

                _cacheService.SetData<ICollection<string>>("currencyCodes", codes, DateTimeOffset.Now.AddYears(1));
            }
            
            return Ok(codes);
        }
    }
}
