using System;
using System.Collections.Generic;
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
    public class SectorsController : Controller
    {
        /*
           These lines are needed to use the Database context,
           define the connection to the API, and use the
           HttpClient to request data from the API
       */
        public ApplicationDbContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /*
             These lines create a Constructor for the HomeController.
             Then, the Database context is defined in a variable.
             Then, an instance of the HttpClient is created.

        */
        public SectorsController(ApplicationDbContext context)
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
        public List<Sector> GetSector()
        {
            List<Sector> sector = new List<Sector>();
            string IEXTrading_API_PATH = BASE_URL + "/stock/market/sector-performance";

            string sectorList = "";
            //List<Divident> dividends1 = new List<Divident>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                sectorList = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!sectorList.Equals(""))
            {

                sector = JsonConvert.DeserializeObject<List<Sector>>(sectorList);
                sector.GetRange(0, sector.Count);
            }
            // dividends.AddRange(dividends1);
            return sector;
        }
       
        public IActionResult Sector()
        {

            ViewBag.dbSuccessComp = 0;
            //List<Company> companies = GetSymbols(symbol);
            List<Sector> sector = GetSector();
            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Sectors"] = JsonConvert.SerializeObject(sector);

            return View(sector);
        }
    }
}