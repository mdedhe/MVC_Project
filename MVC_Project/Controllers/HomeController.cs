using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.DataAccess;
using MVC_Project.Models;
using static MVC_Project.Models.Company;
using static MVC_Project.Models.Divident;
using Newtonsoft.Json;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        /*
            These lines are needed to use the Database context,
            define the connection to the API, and use the
            HttpClient to request data from the API
        */
        static Db dbinsert = new Db();
        public ApplicationDbContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /*
             These lines create a Constructor for the HomeController.
             Then, the Database context is defined in a variable.
             Then, an instance of the HttpClient is created.

        */
        public HomeController(ApplicationDbContext context)
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

        /*
         Adding into database 
         */
        public string AddCompany(Company com)
        {
            var response = dbinsert.Add_companys(com);
            return response;
        }
        public List<Divident> GetDividents(string symbol)
        {
            List<Divident> dividends = new List<Divident>();
            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/dividends/5y";

            string dividentList = "";
            //List<Divident> dividends1 = new List<Divident>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                dividentList = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!dividentList.Equals("") )
            {
               
                dividends = JsonConvert.DeserializeObject<List<Divident>>(dividentList);
                dividends.GetRange(0, dividends.Count );
            }
            // dividends.AddRange(dividends1);
            return dividends;
        }
        //[Route("{id}")]
        public IActionResult Divident(String  id)
        {
            //Set ViewBag variable first
            string symbol = id;
            
            ViewBag.dbSuccessComp = 0;
            //List<Company> companies = GetSymbols(symbol);
            List<Divident> divident= GetDividents(symbol);
             //Save companies in TempData, so they do not have to be retrieved again
            TempData["divident"] = JsonConvert.SerializeObject(divident);

            return View(divident);
        }
        public Logo GetLogos(string symbol)
        {
            Logo logo = new Logo();
            ///stock/{symbol}/logo
            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/logo";

            string logoList = "";
            //List<Divident> dividends1 = new List<Divident>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                logoList = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!logoList.Equals(""))
            {

                logo = JsonConvert.DeserializeObject<Logo>(logoList);
            }
            // dividends.AddRange(dividends1);
            return logo;
        }
        public IActionResult Logo(String symbol)
        {
            ViewBag.dbSuccessComp = 0;
            //List<Company> companies = GetSymbols(symbol);
             Logo logo = GetLogos(symbol);
            //Save companies in TempData, so they do not have to be retrieved again
            TempData["logo"] = JsonConvert.SerializeObject(logo);

            return View(logo);
        }
        public List<Company> GetSymbols()
        {
            string IEXTrading_API_PATH = BASE_URL + "ref-data/symbols";
            string companyList = "";
            List<Company> companies = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                companyList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!companyList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                companies = JsonConvert.DeserializeObject<List<Company>>(companyList);
                companies = companies.GetRange(0, 50);
            }

            return companies;
        }

        //this is for the Get companies
        public IActionResult Index()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Company> companies = GetSymbols();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Companies"] = JsonConvert.SerializeObject(companies);

            return View(companies);
        }

        //
        //this is for the Get companies
        public IActionResult AboutUs()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            return View();
        }
        /*
            The Symbols action calls the GetSymbols method that returns a list of Companies.
            This list of Companies is passed to the Symbols View.
        */
        public IActionResult Symbols()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Company> companies = GetSymbols();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Companies"] = JsonConvert.SerializeObject(companies);

            return View(companies);
        }

        /*
            Save the available symbols in the database
        */
        public IActionResult PopulateSymbols()
        {
            // Retrieve the companies that were saved in the symbols method
            List<Company> companies = JsonConvert.DeserializeObject<List<Company>>(TempData["Companies"].ToString());

            foreach (Company company in companies)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Companies.Where(c => c.Symbol.Equals(company.Symbol)).Count() == 0)
                {
                    dbContext.Companies.Add(company);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("Index", companies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
