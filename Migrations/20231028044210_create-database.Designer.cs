﻿// <auto-generated />
using System;
using Adocao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adocao.Migrations
{
    [DbContext(typeof(AdocaoDevDataContext))]
    [Migration("20231028044210_create-database")]
    partial class createdatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Adocao.Models.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Administrador", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("EspecieId")
                        .HasColumnType("int");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<int>("LocalId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<double?>("Peso")
                        .HasColumnType("float")
                        .HasColumnName("Peso");

                    b.Property<int>("RacaId")
                        .HasColumnType("int");

                    b.Property<string>("Sobre")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("EspecieId");

                    b.HasIndex("LocalId")
                        .IsUnique();

                    b.HasIndex("RacaId");

                    b.ToTable("Animal", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.Especie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Especie", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.Local", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCidade")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Local", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.Raca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("EspecieId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EspecieId");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Raca", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.SolicitacaoAdocaoAnimais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSolicitacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("NomeSolicitante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Solicitacao", (string)null);
                });

            modelBuilder.Entity("Adocao.Models.Animal", b =>
                {
                    b.HasOne("Adocao.Models.Especie", "Especie")
                        .WithMany("Animais")
                        .HasForeignKey("EspecieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adocao.Models.Local", "Local")
                        .WithOne("Animal")
                        .HasForeignKey("Adocao.Models.Animal", "LocalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adocao.Models.Raca", "Raca")
                        .WithMany("Animais")
                        .HasForeignKey("RacaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Especie");

                    b.Navigation("Local");

                    b.Navigation("Raca");
                });

            modelBuilder.Entity("Adocao.Models.Raca", b =>
                {
                    b.HasOne("Adocao.Models.Especie", "Especie")
                        .WithMany("Racas")
                        .HasForeignKey("EspecieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especie");
                });

            modelBuilder.Entity("Adocao.Models.SolicitacaoAdocaoAnimais", b =>
                {
                    b.HasOne("Adocao.Models.Animal", "Animal")
                        .WithMany("Solicitacoes")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("Adocao.Models.Animal", b =>
                {
                    b.Navigation("Solicitacoes");
                });

            modelBuilder.Entity("Adocao.Models.Especie", b =>
                {
                    b.Navigation("Animais");

                    b.Navigation("Racas");
                });

            modelBuilder.Entity("Adocao.Models.Local", b =>
                {
                    b.Navigation("Animal")
                        .IsRequired();
                });

            modelBuilder.Entity("Adocao.Models.Raca", b =>
                {
                    b.Navigation("Animais");
                });
#pragma warning restore 612, 618
        }
    }
}
