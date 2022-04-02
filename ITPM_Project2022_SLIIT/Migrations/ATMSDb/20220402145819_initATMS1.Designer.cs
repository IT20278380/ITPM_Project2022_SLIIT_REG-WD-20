﻿// <auto-generated />
using ITPM_Project2022_SLIIT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITPM_Project2022_SLIIT.Migrations.ATMSDb
{
    [DbContext(typeof(ATMSDbContext))]
    [Migration("20220402145819_initATMS1")]
    partial class initATMS1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITPM_Project2022_SLIIT.Models.FlightList", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BsClassPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EconomyClassPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstClassPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriEconomyClassPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("FlightList");
                });
#pragma warning restore 612, 618
        }
    }
}
