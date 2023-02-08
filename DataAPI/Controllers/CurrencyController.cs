using DataAPI.DataAccess;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Text;
using System.Xml;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyDB _context;

        public CurrencyController(CurrencyDB context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            List<CurrencyRecord> currenciesList = new List<CurrencyRecord>();
            DateTime startDT = DateTime.Now.AddDays(-60);
            DateTime endDT = DateTime.Now;
            

            for (DateTime ystrdy = startDT; ystrdy <= endDT; ystrdy = ystrdy.AddDays(1))
            {

                string infoLink = string.Format("https://www.tcmb.gov.tr/kurlar/{0}.xml",
                                   string.Format("{2}{1}/{0}{1}{2}",
                                        ystrdy.Day.ToString().PadLeft(2, '0'),
                                        ystrdy.Month.ToString().PadLeft(2, '0'),
                                        ystrdy.Year));

                Console.WriteLine(infoLink);
                try
                {
                    XmlDocument XMLlink = new XmlDocument();

                    XMLlink.Load(infoLink);

                    foreach (XmlNode item in XMLlink.SelectNodes("Tarih_Date")[0].ChildNodes)
                    {
                        CurrencyRecord currency = new CurrencyRecord();

                        currency.CurrencyCode = item.Attributes["Kod"].Value;
                        currency.CurrencyName = item["Isim"].InnerText;
                        currency.ForexSelling = Convert.ToDecimal("0" + item["ForexSelling"].InnerText.Replace(".", ","));
                        currency.ForexBuying = Convert.ToDecimal("0" + item["ForexBuying"].InnerText.Replace(".", ","));
                        currency.BanknoteSelling = Convert.ToDecimal("0" + item["BanknoteSelling"].InnerText.Replace(".", ","));
                        currency.BanknoteBuying = Convert.ToDecimal("0" + item["BanknoteBuying"].InnerText.Replace(".", ","));
                        currency.AnnouncedDate = ystrdy;

                        currenciesList.Add(currency);
                    }
                }
                catch (Exception)
                {

                }
            }
            _context.currencyRecords.AddRange(currenciesList);

            _context.SaveChanges();

            return Ok(currenciesList);
        }
    }
}
