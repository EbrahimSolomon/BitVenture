﻿// <auto-generated />
using System;
using BitVenture_EbrahimSolomon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BitVenture_EbrahimSolomon.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230831232825_compute_column_time_breached")]
    partial class compute_column_time_breached
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BitVenture_EbrahimSolomon.Models.DetailRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EffectiveStatusDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MasterRecordID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TimeBreached")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("CASE WHEN DATEDIFF(DAY, TransactionDate, EffectiveStatusDate) > 7 THEN 1 ELSE 0 END");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MasterRecordID");

                    b.ToTable("DetailRecords");
                });

            modelBuilder.Entity("BitVenture_EbrahimSolomon.Models.MasterRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("AccountHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MasterRecords");
                });

            modelBuilder.Entity("BitVenture_EbrahimSolomon.Models.DetailRecord", b =>
                {
                    b.HasOne("BitVenture_EbrahimSolomon.Models.MasterRecord", "MasterRecord")
                        .WithMany("DetailRecords")
                        .HasForeignKey("MasterRecordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterRecord");
                });

            modelBuilder.Entity("BitVenture_EbrahimSolomon.Models.MasterRecord", b =>
                {
                    b.Navigation("DetailRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
