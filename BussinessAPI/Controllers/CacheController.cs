using BussinessAPI.Cache;
using DataAPI.DataAccess;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BussinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
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
            var records = _cacheService.GetData<ICollection<CurrencyRecord>>(currCode);

            if (records==null)
            {
                records = _context.currencyRecords.Where(x=>x.CurrencyCode==currCode).ToList();

                _cacheService.SetData<ICollection<CurrencyRecord>>(currCode, records, DateTimeOffset.Now.AddDays(1));
            }

            return Ok(records);
        }

    }
}
