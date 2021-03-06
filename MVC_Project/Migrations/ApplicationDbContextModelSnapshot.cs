﻿// <auto-generated />
using System;
using MVC_Project.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVC_Project.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVC_Project.Models.Company", b =>
                {
                    b.Property<string>("Symbol")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IexId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsEnabled")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Symbol");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("MVC_Project.Models.CompanyStats", b =>
                {
                    b.Property<string>("Symbol")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<double>("GrossProfit")
                        .HasColumnType("float");

                    b.Property<double>("MarketCap")
                        .HasColumnType("float");

                    b.Property<double>("Revenue")
                        .HasColumnType("float");

                    b.HasKey("Symbol");

                    b.ToTable("CompanyStats");
                });

            modelBuilder.Entity("MVC_Project.Models.Divident", b =>
                {
                    b.Property<DateTime>("Exdate");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Payment_date")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Record_date")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Exdate");

                    b.ToTable("Divident");
                });

            modelBuilder.Entity("MVC_Project.Models.Logo", b =>
                {
                    b.Property<string>("url")
                        .ValueGeneratedOnAdd();

                    b.HasKey("url");

                    b.ToTable("Logo");
                });

            modelBuilder.Entity("MVC_Project.Models.Sector", b =>
                {
                    b.Property<string>("Type")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Performance")
                        .HasColumnType("float");

                    b.Property<string>("lastUpdated")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Type");

                    b.ToTable("Sector");
                });
#pragma warning restore 612, 618
        }
    }
}
