using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    address = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    telephone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialties",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vets",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    birth_date = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    owner_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.id);
                    table.ForeignKey(
                        name: "FK_pets_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pets_types_type_id",
                        column: x => x.type_id,
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vet_specialties",
                columns: table => new
                {
                    SpecialtiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    VetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vet_specialties", x => new { x.SpecialtiesId, x.VetId });
                    table.ForeignKey(
                        name: "FK_vet_specialties_specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "specialties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vet_specialties_vets_VetId",
                        column: x => x.VetId,
                        principalTable: "vets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visits",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    visit_date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    pet_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visits", x => x.id);
                    table.ForeignKey(
                        name: "FK_visits_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "owners_last_name",
                table: "owners",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "IX_pets_owner_id",
                table: "pets",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_type_id",
                table: "pets",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "pets_name",
                table: "pets",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "specialties_name",
                table: "specialties",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "types_name",
                table: "types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_vet_specialties_VetId",
                table: "vet_specialties",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "vets_last_name",
                table: "vets",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "visits_pet_id",
                table: "visits",
                column: "pet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vet_specialties");

            migrationBuilder.DropTable(
                name: "visits");

            migrationBuilder.DropTable(
                name: "specialties");

            migrationBuilder.DropTable(
                name: "vets");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "owners");

            migrationBuilder.DropTable(
                name: "types");
        }
    }
}
