﻿// <auto-generated />
using System;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MargoFetcher.Infrastructure.Migrations
{
    [DbContext(typeof(MargoDbContext))]
    [Migration("20230311134104_Initialize")]
    partial class Initialize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MargoFetcher.Domain.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("charId")
                        .HasColumnType("int");

                    b.Property<DateTime>("fetchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("hid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("lastFetchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rarity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("st")
                        .HasColumnType("int");

                    b.Property<string>("stat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tpl")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MargoFetcher.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("charId")
                        .HasColumnType("int");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<string>("nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profession")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("server")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
