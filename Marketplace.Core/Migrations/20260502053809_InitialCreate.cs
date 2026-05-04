using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOffers_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Budget = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AcceptedJobOfferId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_JobOffers_AcceptedJobOfferId",
                        column: x => x.AcceptedJobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Name",
                table: "Contractors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastName",
                table: "Customers",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_ContractorId",
                table: "JobOffers",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_JobId",
                table: "JobOffers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AcceptedJobOfferId",
                table: "Jobs",
                column: "AcceptedJobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CustomerId",
                table: "Jobs",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Jobs_JobId",
                table: "JobOffers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Contractors_ContractorId",
                table: "JobOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Jobs_JobId",
                table: "JobOffers");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "JobOffers");
        }
    }
}
