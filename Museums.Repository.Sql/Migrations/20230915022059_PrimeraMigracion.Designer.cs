﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Museums.Repository.Sql.Contexts;

#nullable disable

namespace Museums.Repository.Sql.Migrations
{
    [DbContext(typeof(AppDbContextSql))]
    [Migration("20230915022059_PrimeraMigracion")]
    partial class PrimeraMigracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Museums.Core.Entities.CrontabEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DayOfMonth")
                        .HasColumnType("int");

                    b.Property<int?>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int?>("Hour")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivate")
                        .HasColumnType("bit");

                    b.Property<int?>("Minute")
                        .HasColumnType("int");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Crontab");
                });

            modelBuilder.Entity("Museums.Core.Entities.LogEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCancelation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEndExecution")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateExecution")
                        .HasColumnType("datetime2");

                    b.Property<string>("MuseumIdInProcess")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberErrors")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfUpdates")
                        .HasColumnType("int");

                    b.Property<int>("TotalRecords")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Museums.Core.Entities.MuseumEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DatosGenerales")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaDeActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<double>("GmapsLatitud")
                        .HasColumnType("float");

                    b.Property<double>("GmapsLongitud")
                        .HasColumnType("float");

                    b.Property<string>("HoariosYCostos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkSic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocalidadId")
                        .HasColumnType("int");

                    b.Property<int>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("MuseoAdscripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoCalleNumero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoColonia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoCp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoFechaFundacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MuseoId")
                        .HasColumnType("int");

                    b.Property<string>("MuseoNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoTelefono1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoTematicaN1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseoTipoDePropiedad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEnt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomLoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaginaWeb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaginaWeb2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Museo");
                });
#pragma warning restore 612, 618
        }
    }
}
