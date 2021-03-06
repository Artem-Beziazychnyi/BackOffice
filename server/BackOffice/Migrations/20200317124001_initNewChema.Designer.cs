﻿// <auto-generated />
using System;
using BackOffice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackOffice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200317124001_initNewChema")]
    partial class initNewChema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BackOffice.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("BackOffice.Models.BrandQuantityTimeReceived", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeReseived")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("BrandQuantitiesTimeReceived");
                });

            modelBuilder.Entity("BackOffice.Models.BrandQuantityTimeReceived", b =>
                {
                    b.HasOne("BackOffice.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");
                });
#pragma warning restore 612, 618
        }
    }
}
