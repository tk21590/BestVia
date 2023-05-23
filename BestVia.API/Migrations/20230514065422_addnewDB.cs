using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BestVia.API.Migrations
{
    /// <inheritdoc />
    public partial class addnewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<int>(type: "int", nullable: false),
                    DateLastLogin = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Balance_Total = table.Column<long>(type: "bigint", nullable: false),
                    Balance_Used = table.Column<long>(type: "bigint", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefAdd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Block = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTypeDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTypeDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypeDb_CategoryDb_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryTypeId = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryDb_HistoryTypeDb_HistoryTypeId",
                        column: x => x.HistoryTypeId,
                        principalTable: "HistoryTypeDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    PriceIncome = table.Column<long>(type: "bigint", nullable: false),
                    PriceIncomeMod = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Combo = table.Column<int>(type: "int", nullable: false),
                    Sold = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Friend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlDownload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFA = table.Column<bool>(type: "bit", nullable: false),
                    Backup = table.Column<bool>(type: "bit", nullable: false),
                    Change = table.Column<bool>(type: "bit", nullable: false),
                    IsAvaiable = table.Column<bool>(type: "bit", nullable: false),
                    Guarantee = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<int>(type: "int", nullable: false),
                    DateUpdated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDb_ProductTypeDb_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypeDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserOrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    TotalPriceIncome = table.Column<long>(type: "bigint", nullable: false),
                    TotalPriceIncomeMod = table.Column<long>(type: "bigint", nullable: false),
                    DateOrder = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    isComplain = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDb_ProductDb_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDb_StatusDb_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProxyDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProxyDb_ProductDb_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViaDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViaDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViaDb_ProductDb_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<int>(type: "int", nullable: false),
                    DateExpired = table.Column<int>(type: "int", nullable: false),
                    IsAvaiable = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyDb_OrderDb_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "430595c4-f634-417c-974d-efad78c25d91", null, "Customer", "Customer" },
                    { "b1a5d49a-bfe3-4bc8-9ebb-e494b6ef5e8f", null, "SuperMod", "SuperMod" },
                    { "bb8c224b-2ed0-449b-8062-72a39abb2f18", null, "Mod", "Mod" },
                    { "fsd7a9ek-826b-a721-8888-45sjko09mnh2", null, "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApiKey", "Balance", "Balance_Total", "Balance_Used", "Block", "ConcurrencyStamp", "DateCreated", "DateLastLogin", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefAdd", "RefCode", "RoleName", "SecurityStamp", "SubId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8d654b55-7a5f-4273-a379-be4858605ff2", 0, "XwRm59iYWfp40Qsa", 0L, 0L, 0L, false, "e98742c5-e7e3-4c62-b809-1f451037f6b7", 1684072461, 1684073206, null, false, false, null, null, "SuperMod99", "AQAAAAIAAYagAAAAEGaHUFEzWo+PsIqtQ3/SVxi/ajcIsoVG9HjPhGol+vMmVABOY6AIncPaLvVLmgMuOA==", null, false, "", "SuperMod99", "SuperMod", "", 3, false, "SuperMod99" },
                    { "a1069586-b225-4d06-b0ff-89fcea0aa13c", 0, "QZHRRkgfBNDYlzEO", 0L, 0L, 0L, false, "2c8458ca-a995-4d53-8912-220c1faba2b5", 1684072461, 1684073344, null, false, false, null, null, "Moderator99", "AQAAAAIAAYagAAAAEA9x0Fvw/NwAuXkAHk1mBBUvtHwxn6WHZm/vN50tPFn9yKvhr4VGMtM/wZyUmu+DLg==", null, false, "", "Moderator99", "Mod", "", 2, false, "Moderator99" },
                    { "d9500289-5cd5-4fdd-9582-9f85a3f6811d", 0, "pd4n0yWlAr0zvT4o", 0L, 0L, 0L, false, "4bafc402-1e74-453e-b1ff-3fd41864d518", 1684072461, 1684072753, null, false, false, null, null, "Customer99", "AQAAAAIAAYagAAAAEGTJ7VYp9hVGxld38FzFngJvuntYpipM6wGfiqG2QTcAEMy43c9Dq4VqZc1LmLgCVA==", null, false, "ADMIN99", "NZPXYU25", "Customer", "", 4, false, "Customer99" },
                    { "ffd74dbc-81cb-5ipx-917b-67055689754f", 0, "2gCsYVXA58LPeDFU", 0L, 0L, 0L, false, "fb7a595e-b21f-47b8-b3e9-f7ec4457cf01", 1684072461, 1684073122, null, false, false, null, null, "AdminBestMall", "AQAAAAIAAYagAAAAENadmTa7BhFSrTqPjanwtpUjx1eWv6ACJc8ZEis2dE5qWFYINZEissEyn2h7gykQoA==", null, false, "", "Admin99", "Admin", "", 1, false, "AdminBestMall" }
                });

            migrationBuilder.InsertData(
                table: "CategoryDb",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Proxy" },
                    { 2, "Tool" },
                    { 3, "Via" },
                    { 4, "Box" }
                });

            migrationBuilder.InsertData(
                table: "HistoryTypeDb",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nạp tiền" },
                    { 2, "Trừ tiền" },
                    { 3, "Hoàn tiền" },
                    { 4, "Hệ thống" },
                    { 5, "Đặt hàng" }
                });

            migrationBuilder.InsertData(
                table: "StatusDb",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "HOÀN THÀNH" },
                    { 2, "LỖI" },
                    { 3, "HẾT HẠN" },
                    { 4, "ĐANG CHỜ" },
                    { 5, "ĐANG XỬ LÝ" },
                    { 6, "ĐANG CHUẨN BỊ" },
                    { 7, "ĐÃ GIAO HÀNG" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b1a5d49a-bfe3-4bc8-9ebb-e494b6ef5e8f", "8d654b55-7a5f-4273-a379-be4858605ff2" },
                    { "bb8c224b-2ed0-449b-8062-72a39abb2f18", "a1069586-b225-4d06-b0ff-89fcea0aa13c" },
                    { "430595c4-f634-417c-974d-efad78c25d91", "d9500289-5cd5-4fdd-9582-9f85a3f6811d" },
                    { "fsd7a9ek-826b-a721-8888-45sjko09mnh2", "ffd74dbc-81cb-5ipx-917b-67055689754f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDb_HistoryTypeId",
                table: "HistoryDb",
                column: "HistoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyDb_OrderId",
                table: "KeyDb",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDb_ProductId",
                table: "OrderDb",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDb_StatusId",
                table: "OrderDb",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDb_ProductTypeId",
                table: "ProductDb",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeDb_CategoryId",
                table: "ProductTypeDb",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProxyDb_ProductId",
                table: "ProxyDb",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ViaDb_ProductId",
                table: "ViaDb",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DescriptionDb");

            migrationBuilder.DropTable(
                name: "HistoryDb");

            migrationBuilder.DropTable(
                name: "KeyDb");

            migrationBuilder.DropTable(
                name: "ProxyDb");

            migrationBuilder.DropTable(
                name: "ViaDb");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "HistoryTypeDb");

            migrationBuilder.DropTable(
                name: "OrderDb");

            migrationBuilder.DropTable(
                name: "ProductDb");

            migrationBuilder.DropTable(
                name: "StatusDb");

            migrationBuilder.DropTable(
                name: "ProductTypeDb");

            migrationBuilder.DropTable(
                name: "CategoryDb");
        }
    }
}
