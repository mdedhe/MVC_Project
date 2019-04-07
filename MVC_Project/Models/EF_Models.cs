using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
        public class Company
        {
            [Key]
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public bool IsEnabled { get; set; }
            public string Type { get; set; }
            public string IexId { get; set; }
        }
        public class Divident
        {
            [Key]
            public DateTime Exdate { get; set; }
            public DateTime Payment_date { get; set; }
            public DateTime Record_date { get; set; }
            public float? Amount { get; set; }
            public string type { get; set; }
        }
        public class CompanyStats
        {
            [Key]
            public string Symbol { get; set; }
            public string CompanyName { get; set; }
            public float MarketCap { get; set; }
            public float Revenue { get; set; }
            public float GrossProfit { get; set; }
            public float Debt { get; set; }

        }
        public class Sector
        {
            [Key]
            public string Type { get; set; }
            public string Name { get; set; }
            public float Performance { get; set; }
            public string lastUpdated { get; set; }
        }
        public class Logo
        {
            [Key]
            public string url { get; set; }
        }
}
