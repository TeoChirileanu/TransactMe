using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactMe.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_AspNetUserRoles_UserId",
                "AspNetUserRoles");

            migrationBuilder.DropIndex(
                "RoleNameIndex",
                "AspNetRoles");

            migrationBuilder.CreateTable(
                "Transaction",
                table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ClientFirstName = table.Column<string>(nullable: false),
                    ClientLastName = table.Column<string>(nullable: false),
                    ClientSsn = table.Column<string>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    TransactionType = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Transaction", x => x.TransactionId); });

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Transaction");

            migrationBuilder.DropIndex(
                "RoleNameIndex",
                "AspNetRoles");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_UserId",
                "AspNetUserRoles",
                "UserId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName");
        }
    }
}