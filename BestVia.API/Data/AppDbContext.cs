using BestVia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace BestVia.API.Data
{
    public class AppDbContext : IdentityDbContext<IUsers>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            string ADMIN_ID = "ffd74dbc-81cb-5ipx-917b-67055689754f";
            string ADMIN_NAME = "AdminBestMall";
            string ROLE_ADMIN_ID = "fsd7a9ek-826b-a721-8888-45sjko09mnh2";

            string SUPERMOD_ID = Guid.NewGuid().ToString();
            string ROLE_SUPERMOD_ID = Guid.NewGuid().ToString();


            string MOD_ID = Guid.NewGuid().ToString();
            string ROLE_MOD_ID = Guid.NewGuid().ToString();

            string CUSTOMER_ID = Guid.NewGuid().ToString();
            string ROLE_CUSTOMER_ID = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ADMIN_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_SUPERMOD_ID,
                Name = "SuperMod",
                NormalizedName = "SuperMod"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_MOD_ID,
                Name = "Mod",
                NormalizedName = "Mod"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_CUSTOMER_ID,
                Name = "Customer",
                NormalizedName = "Customer"
            });

            var hasher = new PasswordHasher<IUsers>();
            modelBuilder.Entity<IUsers>().HasData(new IUsers
            {
                Id = ADMIN_ID,
                UserName = "AdminBestMall",
                NormalizedUserName = "AdminBestMall",
                EmailConfirmed = false,
                SubId = 1,
                ApiKey = Core.RandomToken(16),
                RefCode = "Admin99",
                RefAdd = "",
                RoleName = "Admin",
                PasswordHash = hasher.HashPassword(null, "Admin@123456@"),
                SecurityStamp = string.Empty,
                DateCreated = Core.Date_GetTimeNowInt(),
                Balance = 0,
                Balance_Total = 0,
                Balance_Used = 0,
                DateLastLogin = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),


            });

            modelBuilder.Entity<IUsers>().HasData(new IUsers
            {
                Id = MOD_ID,
                UserName = "Moderator99",
                NormalizedUserName = "Moderator99",
                EmailConfirmed = false,
                SubId = 2,
                ApiKey = Core.RandomToken(16),
                RefCode = "Moderator99",
                RefAdd = "",
                RoleName = "Mod",
                PasswordHash = hasher.HashPassword(null, "Moderator@123456@"),
                SecurityStamp = string.Empty,
                DateCreated = Core.Date_GetTimeNowInt(),
                Balance = 0,
                Balance_Total = 0,
                Balance_Used = 0,
                DateLastLogin = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),
            });

            modelBuilder.Entity<IUsers>().HasData(new IUsers
            {
                Id = SUPERMOD_ID,
                UserName = "SuperMod99",
                NormalizedUserName = "SuperMod99",
                EmailConfirmed = false,
                SubId = 3,
                ApiKey = Core.RandomToken(16),
                RefCode = "SuperMod99",
                RefAdd = "",

                RoleName = "SuperMod",
                PasswordHash = hasher.HashPassword(null, "SuperMod@123456@"),
                SecurityStamp = string.Empty,
                DateCreated = Core.Date_GetTimeNowInt(),
                Balance = 0,
                Balance_Total = 0,
                Balance_Used = 0,
                DateLastLogin = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),
            });

            modelBuilder.Entity<IUsers>().HasData(new IUsers
            {
                Id = CUSTOMER_ID,
                UserName = "Customer99",
                NormalizedUserName = "Customer99",
                SubId = 4,
                ApiKey = Core.RandomToken(16),
                RefCode = Core.RandomToken(8).ToUpper(),
                RefAdd = "ADMIN99",
                RoleName = "Customer",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Customer@123456@"),
                SecurityStamp = string.Empty,
                DateCreated = Core.Date_GetTimeNowInt(),
                Balance = 0,
                Balance_Total = 0,
                Balance_Used = 0,
                DateLastLogin = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ADMIN_ID,
                UserId = ADMIN_ID
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_MOD_ID,
                UserId = MOD_ID
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_SUPERMOD_ID,
                UserId = SUPERMOD_ID
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_CUSTOMER_ID,
                UserId = CUSTOMER_ID
            });


            //for (int i = 0; i < 100; i++)
            //{
            //    long balance_total = Core.RandomNumber(1, 1000) * 1000;
            //    long balance_used = balance_total - (balance_total / Core.RandomNumber(1, 50));
            //    long balance = balance_total - balance_used;

            //    modelBuilder.Entity<IUsers>().HasData(new IUsers
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        UserName = Core.RandomToken(3) + "_Customer99" + 5 + i,
            //        NormalizedUserName = "Customer99" + 5 + i,
            //        SubId = 5 + i,
            //        ApiKey = Core.RandomToken(16),
            //        RefCode = "REF_" + Core.RandomToken(8).ToUpper(),
            //        RefAdd = "",
            //        RoleName = "Customer",
            //        EmailConfirmed = false,
            //        PasswordHash = hasher.HashPassword(null, "Nothing21590"),
            //        SecurityStamp = string.Empty,
            //        DateCreated = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),
            //        Balance = balance,
            //        Balance_Total = balance_total,
            //        Balance_Used = balance_used,
            //        DateLastLogin = Core.Date_GetTimeNowInt() + Core.RandomNumber(1000, 10000),
            //    });

            //}


            string[] SimStatus = { "HOÀN THÀNH", "LỖI", "HẾT HẠN", "ĐANG CHỜ", "ĐANG XỬ LÝ","ĐANG CHUẨN BỊ","ĐÃ GIAO HÀNG" };
            for (int i = 1; i <= SimStatus.Length; i++)
            {
                modelBuilder.Entity<Status>().HasData(new Status
                {
                    Id = i,
                    Name = SimStatus[i - 1],
                });
            }
            string[] HistoryTypes = { "Nạp tiền", "Trừ tiền", "Hoàn tiền", "Hệ thống", "Đặt hàng" };
            for (int i = 1; i <= HistoryTypes.Length; i++)
            {

                modelBuilder.Entity<HistoryType>().HasData(new HistoryType
                {
                    Id = i,
                    Name = HistoryTypes[i - 1],
                });
            }
            string[] Categories = { "Proxy", "Tool", "Via", "Box" };
            for (int i = 1; i <= Categories.Length; i++)
            {
                modelBuilder.Entity<Category>().HasData(new Category
                {
                    Id = i,
                    Name = Categories[i - 1],
                });
            }

            string[] ProductTypesProxy = { "IP Address V6","IP Address V4",
                "Via Việt Nam","Via Ngoại",
                "Tool MMO","Tool Crawler",
                "Box Phone S7","Box Phone J7 Prime"};
            //for (int i = 1; i <= 1; i++)
            //{
            //    string name = ProductTypesProxy[i - 1];
            //    int cateid = 1;
            //    //{ "Proxy", "Tool", "Via", "Box"
            //    if (name.Contains("Via"))
            //    {
            //        cateid = 3;
            //    }
            //    else if (name.Contains("Proxy"))
            //    {
            //        cateid = 1;

            //    }
            //    else if (name.Contains("Tool"))
            //    {
            //        cateid = 2;

            //    }
            //    else if (name.Contains("Box"))
            //    {
            //        cateid = 4;
            //    }


            //    modelBuilder.Entity<ProductType>().HasData(new ProductType
            //    {
            //        Id = i,
            //        Name = name,
            //        CategoryId = cateid,

            //    });
            //}

            //for (int i = 1; i <= 1; i++)
            //{
            //    var typeProduct = i;
            //    if (i >= 9)
            //    {
            //        typeProduct -= 8;
            //    }
            //    var price = Core.RandomNumber(5, 50) * 1000;
            //    string path_img = "";
            //    string name = ProductTypesProxy[typeProduct - 1];
            //    string download = "";
            //    //{ "Proxy", "Tool", "Via", "Box"
            //    if (name.Contains("Via"))
            //    {
            //        path_img = "https://img3.wallspic.com/attachments/originals/1/9/8/2/4/142891-graphics-pattern-red-technology-line-6080x3420.jpg";
            //    }
            //    else if (name.Contains("Proxy"))
            //    {
            //        path_img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSowIqOMS_zFp5vpAO8DgFwUuZDjH-WZMzyO8x3PpcBZ5I7HDSRrYHP05WI14HsE0TCfMA&usqp=CAU";

            //    }
            //    else if (name.Contains("Tool"))
            //    {
            //        path_img = "https://www.psdgraphics.com/wp-content/uploads/2009/01/3d-software-box.jpg";
            //        download = "https://www.mediafire.com/";

            //    }
            //    else if (name.Contains("Box"))
            //    {
            //        path_img = "https://play-lh.googleusercontent.com/kif95mwH33EVmCgo5HEtExiHMEotoOiMDNVfs2fmIb2LLbsfIrOsNmAlt8bq3xZ_Xuk";
            //    }
            //    modelBuilder.Entity<Product>().HasData(new Product
            //    {
            //        Id = i,
            //        Userid = ADMIN_ID,
            //        Amount = Core.RandomNumber(1, 100) * 10,
            //        IsAvaiable = false,
            //        TwoFA = false,
            //        Backup = false,
            //        Change = false,
            //        CPU = "CPU " + Core.RandomNumber(10, 100) * 1000,
            //        DateCreated = Core.Date_GetTimeNowInt() + Core.RandomNumber(100, 1000),
            //        DateUpdated = Core.Date_GetTimeNowInt() + Core.RandomNumber(1000, 10000),
            //        IP = Guid.NewGuid().ToString(),
            //        Friend = $"{Core.RandomNumber(1, 10) * 100} -{Core.RandomNumber(20, 50) * 100} ",
            //        Guarantee = Core.RandomNumber(1, 100) * 10,
            //        Model = "Model_" + Core.RandomToken(3) + Core.RandomNumberString(3),
            //        ProductTypeId = typeProduct,
            //        Name = name + " " + Core.RandomNumberString(4) + " " + Core.RandomUpcase(4),
            //        Price = price,
            //        Ram = "RAM BUS " + Core.RandomNumberString(6),
            //        Setting = "",
            //        Sold = Core.RandomNumber(50, 500),
            //        Stock = Core.RandomNumber(50, 500),
            //        Username = ADMIN_NAME,
            //        PathImage = path_img,
            //        PriceIncome = (price * 10) / 100,
            //        UrlDownload = download,


            //    });
            //}
            //for (int i = 1; i <= 1; i++)
            //{

            //    bool complain = false;
            //    string reason = "";
            //    var rd = Core.RandomNumber(1, 10);
            //    if (rd % 2 == 0)
            //    {
            //        complain = true;
            //        reason = "Reason_" + Core.RandomNumberString(10);
            //    }
            //    var price = Core.RandomNumber(1, 100) * 10000;

            //    modelBuilder.Entity<Order>().HasData(new Order
            //    {
            //        Id = i,
            //        Address = "Address_" + Core.RandomNumberString(10),
            //        ProductId = 1,
            //        Quantity = Core.RandomNumber(1, 100),
            //        Phone = "09" + Core.RandomNumberString(7),
            //        StatusId = 1,
            //        TotalPrice = price,
            //        User = "Customer",
            //        UserOrderName = "Customer99",
            //        UserOrderId = CUSTOMER_ID,
            //        DateOrder = Core.Date_GetTimeNowInt() - Core.RandomNumber(100, 1000),
            //        isComplain = complain,
            //        Reason = reason,
            //        TotalPriceIncome = (price * 20) / 100,
            //        TotalPriceIncomeMod = (price * 10) / 100
            //    }); 
            //}
            //for (int i = 1; i <= 1; i++)
            //{
            //    modelBuilder.Entity<Key>().HasData(new Key
            //    {
            //        Id = i,
            //        DateCreated = Core.Date_GetTimeNowInt(),
            //        DateExpired = Core.Date_GetTimeNowInt() + Core.RandomNumber(10000, 100000),
            //        KeyId = Guid.NewGuid().ToString(),
            //        KeyName = "Tool MMO " + Core.RandomNumber(1000, 9999),
            //        OrderId = 1,
            //        IsAvaiable = true,
            //        UserId = CUSTOMER_ID,
            //        UserName = "Customer99",
            //        ProductId = 1,
            //    });
            //}

            //for (int i = 1; i <= 1; i++)
            //{
            //    modelBuilder.Entity<Via>().HasData(new Via
            //    {
            //        Id = i,
            //        IsSold = false,
            //        Mail = "Mail_" + Core.RandomToken(7) + "@gmail.com",
            //        PasswordMail = Core.RandomToken(7).ToLower(),
            //        Password = Core.RandomToken(12).ToLower(),
            //        UID = Core.RandomNumberString(15),
            //        TwoFa = Guid.NewGuid().ToString().ToLower(),
            //        Userid = ADMIN_ID,
            //        Username = ADMIN_NAME,
            //        ProductId = 1,
            //        User = "Nguyễn Anh",
            //    });
            //}

            //for (int i = 1; i <= 200; i++)
            //{
            //    modelBuilder.Entity<Proxy>().HasData(new Proxy
            //    {
            //        Id = i,

            //        Userid = ADMIN_ID,
            //        Username = ADMIN_NAME,
            //        ProductId = 1,
            //        OrderId = 1,
            //        IsSold = false,
            //        Name = $"{Core.RandomNumberString(3)}.{Core.RandomNumberString(3)}.{Core.RandomNumberString(3)}.{Core.RandomNumberString(2)}:{Core.RandomNumberString(4)}"

            //    });
            //}

        }


        public DbSet<Via> ViaDb { get; set; }
        public DbSet<Product> ProductDb { get; set; }
        public DbSet<ProductType> ProductTypeDb { get; set; }
        public DbSet<Order> OrderDb { get; set; }
        public DbSet<Category> CategoryDb { get; set; }
        public DbSet<Status> StatusDb { get; set; }
        public DbSet<History> HistoryDb { get; set; }
        public DbSet<HistoryType> HistoryTypeDb { get; set; }
        public DbSet<Key> KeyDb { get; set; }
        public DbSet<Proxy> ProxyDb { get; set; }
        public DbSet<Description> DescriptionDb { get; set; }
    }
}
