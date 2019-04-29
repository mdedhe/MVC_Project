using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MVC_Project.Models
{
   /*public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Divident> Divident { get; set; }
        public DbSet<CompanyStats> CompanyStats { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Logo> Logo { get; set; }
    }*/
    public class Company
    {
        [Key]
        public string Symbol { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Date { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public bool IsEnabled { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string IexId { get; set; }
    }
    public class Divident
    {
        [Key]
        public DateTime Exdate { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public DateTime Payment_date { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public DateTime Record_date { get; set; }

        [Column(TypeName = "float")]
        public float? Amount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string type { get; set; }
    }
    public class CompanyStats
    {
        [Key]
        public string Symbol { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "float")]
        public float MarketCap { get; set; }

        [Column(TypeName = "float")]
        public float Revenue { get; set; }

        [Column(TypeName = "float")]
        public float GrossProfit { get; set; }

        [Column(TypeName = "float")]
        public float Debt { get; set; }

    }
    public class Sector
    {
        [Key]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        [Column(TypeName = "float")]
        public float Performance { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string lastUpdated { get; set; }
    }
    public class Logo
    {
        [Key]
        public string url { get; set; }
    }
}
