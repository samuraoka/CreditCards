﻿// <auto-generated />
using CreditCards.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CreditCards.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180311024343_AddedColumn")]
    partial class AddedColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CreditCards.Core.Model.CreditCardApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName");

                    b.Property<string>("FrequentFlyerNumber");

                    b.Property<decimal>("GrossAnnualIncome");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("CreditCardApplication");
                });
#pragma warning restore 612, 618
        }
    }
}