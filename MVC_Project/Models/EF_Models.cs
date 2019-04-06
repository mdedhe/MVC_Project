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
            public string Exdate { get; set; }
            public string Payment_date { get; set; }
            public string Record_date { get; set; }
            public float Amount { get; set; }
            public string Divident_type { get; set; }
        }
}
