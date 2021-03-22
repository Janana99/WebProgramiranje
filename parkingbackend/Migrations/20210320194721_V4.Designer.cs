﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parkingbackend.Models;

namespace parkingbackend.Migrations
{
    [DbContext(typeof(ParkingContext))]
    [Migration("20210320194721_V4")]
    partial class V4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parkingbackend.Models.Automobil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Automobilibroj")
                        .HasColumnType("int")
                        .HasColumnName("Automobilibroj");

                    b.Property<int>("Idpolja")
                        .HasColumnType("int")
                        .HasColumnName("Idpolja");

                    b.Property<int?>("PoljeID")
                        .HasColumnType("int");

                    b.Property<string>("Tip")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Tip");

                    b.HasKey("ID");

                    b.HasIndex("PoljeID");

                    b.ToTable("Automobil");
                });

            modelBuilder.Entity("Parkingbackend.Models.Parking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ime");

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int")
                        .HasColumnName("Kapacitet");

                    b.Property<int>("M")
                        .HasColumnType("int")
                        .HasColumnName("M");

                    b.Property<int>("N")
                        .HasColumnType("int")
                        .HasColumnName("N");

                    b.HasKey("ID");

                    b.ToTable("Parking");
                });

            modelBuilder.Entity("Parkingbackend.Models.Polje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Boja")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Boja");

                    b.Property<int>("Brojautomobila")
                        .HasColumnType("int")
                        .HasColumnName("Brojautomobila");

                    b.Property<int>("Idparkinga")
                        .HasColumnType("int")
                        .HasColumnName("Idparkinga");

                    b.Property<int>("Maxkapacitet")
                        .HasColumnType("int")
                        .HasColumnName("Maxkapacitet");

                    b.Property<string>("Nazivpolja")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Nazivpolja");

                    b.Property<int?>("ParkingID")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int")
                        .HasColumnName("X");

                    b.Property<int>("Y")
                        .HasColumnType("int")
                        .HasColumnName("Y");

                    b.HasKey("ID");

                    b.HasIndex("ParkingID");

                    b.ToTable("Polje");
                });

            modelBuilder.Entity("Parkingbackend.Models.Automobil", b =>
                {
                    b.HasOne("Parkingbackend.Models.Polje", "Polje")
                        .WithMany("Automobili")
                        .HasForeignKey("PoljeID");

                    b.Navigation("Polje");
                });

            modelBuilder.Entity("Parkingbackend.Models.Polje", b =>
                {
                    b.HasOne("Parkingbackend.Models.Parking", "Parking")
                        .WithMany("ParkingPolja")
                        .HasForeignKey("ParkingID");

                    b.Navigation("Parking");
                });

            modelBuilder.Entity("Parkingbackend.Models.Parking", b =>
                {
                    b.Navigation("ParkingPolja");
                });

            modelBuilder.Entity("Parkingbackend.Models.Polje", b =>
                {
                    b.Navigation("Automobili");
                });
#pragma warning restore 612, 618
        }
    }
}
