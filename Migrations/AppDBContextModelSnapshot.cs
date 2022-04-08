﻿// <auto-generated />
using System;
using FindJobWebApi.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FindJobWebApi.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FindJobWebApi.Models.Candidtate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("VacancyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("VacancyId1")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VacancyId1");

                    b.ToTable("Candidtates");
                });

            modelBuilder.Entity("FindJobWebApi.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyAddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Desciption")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyAddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FindJobWebApi.Models.CompanyAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressFirst")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressSecond")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CompanyAddresses");
                });

            modelBuilder.Entity("FindJobWebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthdayDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Desciption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Experience")
                        .HasColumnType("real");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserAddressId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserAddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FindJobWebApi.Models.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressFirst")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressSecond")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("FindJobWebApi.Models.Vacancy", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Responsibilities")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("FindJobWebApi.Models.Candidtate", b =>
                {
                    b.HasOne("FindJobWebApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindJobWebApi.Models.Vacancy", "Vacancy")
                        .WithMany()
                        .HasForeignKey("VacancyId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("FindJobWebApi.Models.Company", b =>
                {
                    b.HasOne("FindJobWebApi.Models.CompanyAddress", "CompanyAddress")
                        .WithMany()
                        .HasForeignKey("CompanyAddressId");

                    b.Navigation("CompanyAddress");
                });

            modelBuilder.Entity("FindJobWebApi.Models.User", b =>
                {
                    b.HasOne("FindJobWebApi.Models.UserAddress", "UserAddress")
                        .WithMany()
                        .HasForeignKey("UserAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAddress");
                });

            modelBuilder.Entity("FindJobWebApi.Models.Vacancy", b =>
                {
                    b.HasOne("FindJobWebApi.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
