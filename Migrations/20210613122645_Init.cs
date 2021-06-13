﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace invoice_manager.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine2 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxNumber = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IBAN = table.Column<string>(type: "char(28)", fixedLength: true, maxLength: 28, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 226, DateTimeKind.Local).AddTicks(497)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 226, DateTimeKind.Local).AddTicks(1049))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.UniqueConstraint("AK_Clients_Name", x => x.Name);
                    table.UniqueConstraint("AK_Clients_TaxNumber", x => x.TaxNumber);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine2 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxNumber = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IBAN = table.Column<string>(type: "char(28)", fixedLength: true, maxLength: 28, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "char(9)", fixedLength: true, maxLength: 9, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoSourcePath = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 226, DateTimeKind.Local).AddTicks(4615)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 226, DateTimeKind.Local).AddTicks(5093))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.UniqueConstraint("AK_Companies_Name", x => x.Name);
                    table.UniqueConstraint("AK_Companies_TaxNumber", x => x.TaxNumber);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Multiplier = table.Column<float>(type: "float(2)", precision: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 227, DateTimeKind.Local).AddTicks(3303)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 227, DateTimeKind.Local).AddTicks(3769))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                    table.UniqueConstraint("AK_Taxes_Multiplier", x => x.Multiplier);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "char(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 219, DateTimeKind.Local).AddTicks(6762)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 225, DateTimeKind.Local).AddTicks(4255))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PricePerUnit = table.Column<float>(type: "float(2)", precision: 2, nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 227, DateTimeKind.Local).AddTicks(7000)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 227, DateTimeKind.Local).AddTicks(7997))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateDue = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IssuedById = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 229, DateTimeKind.Local).AddTicks(3758)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 229, DateTimeKind.Local).AddTicks(4291))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductsLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", precision: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 228, DateTimeKind.Local).AddTicks(583)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 14, 26, 45, 228, DateTimeKind.Local).AddTicks(1062))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsLists_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "Multiplier" },
                values: new object[,]
                {
                    { 1, 0.23f },
                    { 2, 0.08f },
                    { 3, 0.07f },
                    { 4, 0.04f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { 1, "admin@domain.com", "Admin", "System", "0xc" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssuedById",
                table: "Invoices",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OwnerId",
                table: "Products",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxId",
                table: "Products",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLists_InvoiceId",
                table: "ProductsLists",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLists_ProductId",
                table: "ProductsLists",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsLists");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Taxes");
        }
    }
}
