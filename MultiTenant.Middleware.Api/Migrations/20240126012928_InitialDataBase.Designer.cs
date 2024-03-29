﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiTenant.Middleware.Api.Repository;

#nullable disable

namespace MultiTenant.Middleware.Api.Migrations
{
    [DbContext(typeof(MultiTenantContext))]
    [Migration("20240126012928_InitialDataBase")]
    partial class InitialDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("MultiTenant.Middleware.Api.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Product 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Product 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Product 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
