﻿// <auto-generated />
using System;
using ChickenFarmApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChickenFarmApi.DataAccess.Migrations
{
    [DbContext(typeof(ChickenFarmContext))]
    [Migration("20231126223936_AddEggLayingRecordForeignKey")]
    partial class AddEggLayingRecordForeignKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("ChickenFarmApi.DataAccess.Entities.Chicken", b =>
                {
                    b.Property<int>("ChickenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ChickenId");

                    b.ToTable("Chickens");
                });

            modelBuilder.Entity("ChickenFarmApi.DataAccess.Entities.EggLayingRecord", b =>
                {
                    b.Property<Guid>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ChickenId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EggCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("RecordId");

                    b.HasIndex("ChickenId");

                    b.ToTable("EggLayingRecords");
                });

            modelBuilder.Entity("ChickenFarmApi.DataAccess.Entities.EggLayingRecord", b =>
                {
                    b.HasOne("ChickenFarmApi.DataAccess.Entities.Chicken", "Chicken")
                        .WithMany("EggLayingRecords")
                        .HasForeignKey("ChickenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chicken");
                });

            modelBuilder.Entity("ChickenFarmApi.DataAccess.Entities.Chicken", b =>
                {
                    b.Navigation("EggLayingRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
