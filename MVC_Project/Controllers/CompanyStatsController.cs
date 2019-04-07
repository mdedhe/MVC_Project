using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.DataAccess;
using MVC_Project.Models;
using Newtonsoft.Json;

namespace MVC_Project.Controllers
{
    
    public class CompanyStatsController : Controller
    {
        public ApplicationDbContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /*
             These lines create a Constructor for the HomeController.
             Then, the Database context is defined in a variable.
             Then, an instance of the HttpClient is created.

        */
        public CompanyStatsController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*
            Calls the IEX reference API to get the list of symbols.
            Returns a list of the companies whose information is available. 
        */
        public CompanyStats GetCompanyStats(string symbol)
        {

        CompanyStats companyStats = new CompanyStats();
        string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/stats";

        string statList = "";
        //List<Divident> dividends1 = new List<Divident>();
          httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
        
        HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

          if (respose.IsSuccessStatusCode)
            {
                  statList = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
                  if (!statList.Equals(""))
                    {

                    companyStats = JsonConvert.DeserializeObject<CompanyStats>(statList);
                      //companyStats.GetRange(0, companyStats.Le);
                    }
         //dividends.AddRange(dividends1);
          return companyStats;

        }
        //[Route("{Name}/{id}")]
        public IActionResult CompanyStat(string Name,string id)
        {
                  string symbol = id;
                ViewBag.dbSuccessComp = 0;
              CompanyStats companystat = GetCompanyStats(symbol);
            TempData["companystat"] = JsonConvert.SerializeObject(companystat);
          return View(companystat);

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}