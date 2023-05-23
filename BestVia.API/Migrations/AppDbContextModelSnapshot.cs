﻿// <auto-generated />
using System;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BestVia.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BestVia.API.Data.IUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<long>("Balance_Total")
                        .HasColumnType("bigint");

                    b.Property<long>("Balance_Used")
                        .HasColumnType("bigint");

                    b.Property<bool>("Block")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateCreated")
                        .HasColumnType("int");

                    b.Property<int>("DateLastLogin")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefAdd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ffd74dbc-81cb-5ipx-917b-67055689754f",
                            AccessFailedCount = 0,
                            ApiKey = "2gCsYVXA58LPeDFU",
                            Balance = 0L,
                            Balance_Total = 0L,
                            Balance_Used = 0L,
                            Block = false,
                            ConcurrencyStamp = "fb7a595e-b21f-47b8-b3e9-f7ec4457cf01",
                            DateCreated = 1684072461,
                            DateLastLogin = 1684073122,
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "AdminBestMall",
                            PasswordHash = "AQAAAAIAAYagAAAAENadmTa7BhFSrTqPjanwtpUjx1eWv6ACJc8ZEis2dE5qWFYINZEissEyn2h7gykQoA==",
                            PhoneNumberConfirmed = false,
                            RefAdd = "",
                            RefCode = "Admin99",
                            RoleName = "Admin",
                            SecurityStamp = "",
                            SubId = 1,
                            TwoFactorEnabled = false,
                            UserName = "AdminBestMall"
                        },
                        new
                        {
                            Id = "a1069586-b225-4d06-b0ff-89fcea0aa13c",
                            AccessFailedCount = 0,
                            ApiKey = "QZHRRkgfBNDYlzEO",
                            Balance = 0L,
                            Balance_Total = 0L,
                            Balance_Used = 0L,
                            Block = false,
                            ConcurrencyStamp = "2c8458ca-a995-4d53-8912-220c1faba2b5",
                            DateCreated = 1684072461,
                            DateLastLogin = 1684073344,
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "Moderator99",
                            PasswordHash = "AQAAAAIAAYagAAAAEA9x0Fvw/NwAuXkAHk1mBBUvtHwxn6WHZm/vN50tPFn9yKvhr4VGMtM/wZyUmu+DLg==",
                            PhoneNumberConfirmed = false,
                            RefAdd = "",
                            RefCode = "Moderator99",
                            RoleName = "Mod",
                            SecurityStamp = "",
                            SubId = 2,
                            TwoFactorEnabled = false,
                            UserName = "Moderator99"
                        },
                        new
                        {
                            Id = "8d654b55-7a5f-4273-a379-be4858605ff2",
                            AccessFailedCount = 0,
                            ApiKey = "XwRm59iYWfp40Qsa",
                            Balance = 0L,
                            Balance_Total = 0L,
                            Balance_Used = 0L,
                            Block = false,
                            ConcurrencyStamp = "e98742c5-e7e3-4c62-b809-1f451037f6b7",
                            DateCreated = 1684072461,
                            DateLastLogin = 1684073206,
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "SuperMod99",
                            PasswordHash = "AQAAAAIAAYagAAAAEGaHUFEzWo+PsIqtQ3/SVxi/ajcIsoVG9HjPhGol+vMmVABOY6AIncPaLvVLmgMuOA==",
                            PhoneNumberConfirmed = false,
                            RefAdd = "",
                            RefCode = "SuperMod99",
                            RoleName = "SuperMod",
                            SecurityStamp = "",
                            SubId = 3,
                            TwoFactorEnabled = false,
                            UserName = "SuperMod99"
                        },
                        new
                        {
                            Id = "d9500289-5cd5-4fdd-9582-9f85a3f6811d",
                            AccessFailedCount = 0,
                            ApiKey = "pd4n0yWlAr0zvT4o",
                            Balance = 0L,
                            Balance_Total = 0L,
                            Balance_Used = 0L,
                            Block = false,
                            ConcurrencyStamp = "4bafc402-1e74-453e-b1ff-3fd41864d518",
                            DateCreated = 1684072461,
                            DateLastLogin = 1684072753,
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "Customer99",
                            PasswordHash = "AQAAAAIAAYagAAAAEGTJ7VYp9hVGxld38FzFngJvuntYpipM6wGfiqG2QTcAEMy43c9Dq4VqZc1LmLgCVA==",
                            PhoneNumberConfirmed = false,
                            RefAdd = "ADMIN99",
                            RefCode = "NZPXYU25",
                            RoleName = "Customer",
                            SecurityStamp = "",
                            SubId = 4,
                            TwoFactorEnabled = false,
                            UserName = "Customer99"
                        });
                });

            modelBuilder.Entity("BestVia.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryDb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Proxy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tool"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Via"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Box"
                        });
                });

            modelBuilder.Entity("BestVia.Models.Description", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DescriptionDb");
                });

            modelBuilder.Entity("BestVia.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateCreated")
                        .HasColumnType("int");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HistoryTypeId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HistoryTypeId");

                    b.ToTable("HistoryDb");
                });

            modelBuilder.Entity("BestVia.Models.HistoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HistoryTypeDb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Nạp tiền"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Trừ tiền"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hoàn tiền"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Hệ thống"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Đặt hàng"
                        });
                });

            modelBuilder.Entity("BestVia.Models.Key", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DateCreated")
                        .HasColumnType("int");

                    b.Property<int>("DateExpired")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvaiable")
                        .HasColumnType("bit");

                    b.Property<string>("KeyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("KeyDb");
                });

            modelBuilder.Entity("BestVia.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BillOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateOrder")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalPriceIncome")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalPriceIncomeMod")
                        .HasColumnType("bigint");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserOrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserOrderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isComplain")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StatusId");

                    b.ToTable("OrderDb");
                });

            modelBuilder.Entity("BestVia.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<bool>("Backup")
                        .HasColumnType("bit");

                    b.Property<string>("CPU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Change")
                        .HasColumnType("bit");

                    b.Property<int>("Combo")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateCreated")
                        .HasColumnType("int");

                    b.Property<int>("DateUpdated")
                        .HasColumnType("int");

                    b.Property<string>("Friend")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Guarantee")
                        .HasColumnType("int");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvaiable")
                        .HasColumnType("bit");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<long>("PriceIncome")
                        .HasColumnType("bigint");

                    b.Property<long>("PriceIncomeMod")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Ram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setting")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sold")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFA")
                        .HasColumnType("bit");

                    b.Property<string>("UrlDownload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("ProductDb");
                });

            modelBuilder.Entity("BestVia.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductTypeDb");
                });

            modelBuilder.Entity("BestVia.Models.Proxy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSold")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProxyDb");
                });

            modelBuilder.Entity("BestVia.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusDb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "HOÀN THÀNH"
                        },
                        new
                        {
                            Id = 2,
                            Name = "LỖI"
                        },
                        new
                        {
                            Id = 3,
                            Name = "HẾT HẠN"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ĐANG CHỜ"
                        },
                        new
                        {
                            Id = 5,
                            Name = "ĐANG XỬ LÝ"
                        },
                        new
                        {
                            Id = 6,
                            Name = "ĐANG CHUẨN BỊ"
                        },
                        new
                        {
                            Id = 7,
                            Name = "ĐÃ GIAO HÀNG"
                        });
                });

            modelBuilder.Entity("BestVia.Models.Via", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSold")
                        .HasColumnType("bit");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("TwoFa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ViaDb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "fsd7a9ek-826b-a721-8888-45sjko09mnh2",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "b1a5d49a-bfe3-4bc8-9ebb-e494b6ef5e8f",
                            Name = "SuperMod",
                            NormalizedName = "SuperMod"
                        },
                        new
                        {
                            Id = "bb8c224b-2ed0-449b-8062-72a39abb2f18",
                            Name = "Mod",
                            NormalizedName = "Mod"
                        },
                        new
                        {
                            Id = "430595c4-f634-417c-974d-efad78c25d91",
                            Name = "Customer",
                            NormalizedName = "Customer"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "ffd74dbc-81cb-5ipx-917b-67055689754f",
                            RoleId = "fsd7a9ek-826b-a721-8888-45sjko09mnh2"
                        },
                        new
                        {
                            UserId = "a1069586-b225-4d06-b0ff-89fcea0aa13c",
                            RoleId = "bb8c224b-2ed0-449b-8062-72a39abb2f18"
                        },
                        new
                        {
                            UserId = "8d654b55-7a5f-4273-a379-be4858605ff2",
                            RoleId = "b1a5d49a-bfe3-4bc8-9ebb-e494b6ef5e8f"
                        },
                        new
                        {
                            UserId = "d9500289-5cd5-4fdd-9582-9f85a3f6811d",
                            RoleId = "430595c4-f634-417c-974d-efad78c25d91"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BestVia.Models.History", b =>
                {
                    b.HasOne("BestVia.Models.HistoryType", "HistoryType")
                        .WithMany()
                        .HasForeignKey("HistoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoryType");
                });

            modelBuilder.Entity("BestVia.Models.Key", b =>
                {
                    b.HasOne("BestVia.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BestVia.Models.Order", b =>
                {
                    b.HasOne("BestVia.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BestVia.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("BestVia.Models.Product", b =>
                {
                    b.HasOne("BestVia.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("BestVia.Models.ProductType", b =>
                {
                    b.HasOne("BestVia.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BestVia.Models.Proxy", b =>
                {
                    b.HasOne("BestVia.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BestVia.Models.Via", b =>
                {
                    b.HasOne("BestVia.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BestVia.API.Data.IUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BestVia.API.Data.IUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BestVia.API.Data.IUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BestVia.API.Data.IUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}