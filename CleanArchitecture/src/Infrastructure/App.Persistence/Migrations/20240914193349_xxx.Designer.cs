﻿// <auto-generated />
using System;
using App.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240914193349_xxx")]
    partial class xxx
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("App.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App.Domain.Entities.Product", b =>
                {
                    b.HasOne("App.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("App.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
