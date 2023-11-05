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
    [Migration("20231030193157_CreateDatabase")]
    partial class CreateDatabase
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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("b49d7f97-626a-4880-b467-de1950b7b46e"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("FotoId")
                        .HasColumnType("int");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Peso")
                        .HasColumnType("float")
                        .HasColumnName("Peso");

                    b.Property<string>("Porte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RacaId")
                        .HasColumnType("int");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobre")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("EspecieId");

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

            modelBuilder.Entity("Adocao.Models.Foto", b =>
                {
                    b.Property<int>("FotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FotoId"), 1L, 1);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FotoId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Foto", (string)null);
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

            modelBuilder.Entity("Adocao.Models.RecuperacaoSenha", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("c703c5e9-1725-4d4c-ba2e-36a5b7104a2e"));

                    b.Property<Guid>("AdministradorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expiration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 10, 30, 20, 31, 56, 958, DateTimeKind.Utc).AddTicks(6688));

                    b.HasKey("Id");

                    b.HasIndex("AdministradorId")
                        .IsUnique();

                    b.ToTable("RecuperacaoSenhas");
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
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

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

                    b.HasOne("Adocao.Models.Raca", "Raca")
                        .WithMany("Animais")
                        .HasForeignKey("RacaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Especie");

                    b.Navigation("Raca");
                });

            modelBuilder.Entity("Adocao.Models.Foto", b =>
                {
                    b.HasOne("Adocao.Models.Animal", "Animal")
                        .WithMany("Fotos")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
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

            modelBuilder.Entity("Adocao.Models.RecuperacaoSenha", b =>
                {
                    b.HasOne("Adocao.Models.Administrador", "Administrador")
                        .WithOne("Recuperacao")
                        .HasForeignKey("Adocao.Models.RecuperacaoSenha", "AdministradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrador");
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

            modelBuilder.Entity("Adocao.Models.Administrador", b =>
                {
                    b.Navigation("Recuperacao")
                        .IsRequired();
                });

            modelBuilder.Entity("Adocao.Models.Animal", b =>
                {
                    b.Navigation("Fotos");

                    b.Navigation("Solicitacoes");
                });

            modelBuilder.Entity("Adocao.Models.Especie", b =>
                {
                    b.Navigation("Animais");

                    b.Navigation("Racas");
                });

            modelBuilder.Entity("Adocao.Models.Raca", b =>
                {
                    b.Navigation("Animais");
                });
#pragma warning restore 612, 618
        }
    }
}