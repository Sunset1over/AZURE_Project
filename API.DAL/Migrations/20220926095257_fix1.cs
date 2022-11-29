using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.DAL.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    BreathingRate = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_AnimalId",
                table: "Measurements",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
