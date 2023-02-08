﻿// <auto-generated />
using System;
using DataAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAPI.Migrations
{
    [DbContext(typeof(CurrencyDB))]
    [Migration("20230207194928_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataAPI.Models.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Code");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("DataAPI.Models.CurrencyRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AnnouncedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("BanknoteBuying")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BanknoteSelling")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ForexBuying")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ForexSelling")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("currencyRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
