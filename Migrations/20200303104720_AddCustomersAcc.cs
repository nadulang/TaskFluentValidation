using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskMediatrFluentValidation.Migrations
{
    public partial class AddCustomersAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersAcc",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    birthdate = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    gender = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersAcc", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    rating = table.Column<double>(nullable: false),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<int>(nullable: false),
                    name_on_card = table.Column<string>(nullable: true),
                    exp_month = table.Column<string>(nullable: true),
                    exp_year = table.Column<string>(nullable: true),
                    postal_code = table.Column<int>(nullable: false),
                    credit_card_number = table.Column<string>(nullable: true),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cards_CustomersAcc_customer_id",
                        column: x => x.customer_id,
                        principalTable: "CustomersAcc",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productsbuy",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    merchant_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productsbuy", x => x.id);
                    table.ForeignKey(
                        name: "FK_Productsbuy_Merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "Merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_customer_id",
                table: "Cards",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Productsbuy_merchant_id",
                table: "Productsbuy",
                column: "merchant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Productsbuy");

            migrationBuilder.DropTable(
                name: "CustomersAcc");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
