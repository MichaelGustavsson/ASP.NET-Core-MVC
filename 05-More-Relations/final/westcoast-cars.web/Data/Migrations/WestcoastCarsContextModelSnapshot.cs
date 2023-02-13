﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using westcoast_cars.web.Data;

#nullable disable

namespace westcoastcars.web.Data.Migrations
{
    [DbContext(typeof(WestcoastCarsContext))]
    partial class WestcoastCarsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("westcoast_cars.web.Models.FuelTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.ManufacturerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.TransmissionsTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TransmissionsTypes");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FuelTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MakeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Mileage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModelYear")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransmissionsTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("MakeId");

                    b.HasIndex("TransmissionsTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.VehicleModel", b =>
                {
                    b.HasOne("westcoast_cars.web.Models.FuelTypeModel", "FuelType")
                        .WithMany("Vehicles")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("westcoast_cars.web.Models.ManufacturerModel", "Manufacturer")
                        .WithMany("Vehicles")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("westcoast_cars.web.Models.TransmissionsTypeModel", "TransmissionsType")
                        .WithMany("Vehicles")
                        .HasForeignKey("TransmissionsTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelType");

                    b.Navigation("Manufacturer");

                    b.Navigation("TransmissionsType");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.FuelTypeModel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.ManufacturerModel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("westcoast_cars.web.Models.TransmissionsTypeModel", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
