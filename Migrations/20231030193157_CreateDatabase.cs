using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adocao.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("b49d7f97-626a-4880-b467-de1950b7b46e")),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecuperacaoSenhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("c703c5e9-1725-4d4c-ba2e-36a5b7104a2e")),
                    AdministradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 30, 20, 31, 56, 958, DateTimeKind.Utc).AddTicks(6688))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecuperacaoSenhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecuperacaoSenhas_Administrador_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "Administrador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Raca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    EspecieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raca_Especie_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: true),
                    Sobre = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoId = table.Column<int>(type: "int", nullable: false),
                    EspecieId = table.Column<int>(type: "int", nullable: false),
                    RacaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animal_Especie_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animal_Raca_RacaId",
                        column: x => x.RacaId,
                        principalTable: "Raca",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    FotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_Foto_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_Email",
                table: "Administrador",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_EspecieId",
                table: "Animal",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_RacaId",
                table: "Animal",
                column: "RacaId");

            migrationBuilder.CreateIndex(
                name: "IX_Especie_Nome",
                table: "Especie",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foto_AnimalId",
                table: "Foto",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Raca_EspecieId",
                table: "Raca",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_Raca_Nome",
                table: "Raca",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecuperacaoSenhas_AdministradorId",
                table: "RecuperacaoSenhas",
                column: "AdministradorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_AnimalId",
                table: "Solicitacao",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "RecuperacaoSenhas");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Raca");

            migrationBuilder.DropTable(
                name: "Especie");
        }
    }
}
