﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DomainDbContext))]
    partial class DomainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Domain.Models.Begehung", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Begehungen");
                });

            modelBuilder.Entity("Domain.Models.Begehungsobjekt", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Bauwerk")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Begehungsobjekt");
                });

            modelBuilder.Entity("Domain.Models.Mitarbeiter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Mitarbeiter");
                });

            modelBuilder.Entity("Domain.Models.Prüfling", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("longtext");

                    b.Property<string>("Haus")
                        .HasColumnType("longtext");

                    b.Property<string>("Ort")
                        .HasColumnType("longtext");

                    b.Property<string>("Straße")
                        .HasColumnType("longtext");

                    b.Property<int>("Typ")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Prüfling");
                });
#pragma warning restore 612, 618
        }
    }
}
