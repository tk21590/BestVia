using BestVia.API.Data;
using BestVia.API.Services;
using BestVia.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BestVia.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                         options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
//=============//
builder.Services.AddAutoMapper(typeof(Program).Assembly);




//builder.Services.AddHttpClient<IAuthServicesAPI, AuthServicesAPI>();
builder.Services.AddHttpClient<IUserServicesAPI, UserServicesAPI>();
builder.Services.AddHttpClient<ICategoryServicesAPI, CategoryServicesAPI>();
builder.Services.AddHttpClient<IOrderServicesAPI, OrderServicesAPI>();
builder.Services.AddHttpClient<IProductTypeServicesAPI, ProductTypeServicesAPI>();
builder.Services.AddHttpClient<IProductServicesAPI, ProductServicesAPI>();
builder.Services.AddHttpClient<IViaServicesAPI, ViaServicesAPI>();
builder.Services.AddHttpClient<ISummaryServicesAPI, SummaryServicesAPI>();
builder.Services.AddHttpClient<IKeyServicesAPI, KeyServicesAPI>();
builder.Services.AddHttpClient<IProxyServicesAPI, ProxyServicesAPI>();
builder.Services.AddHttpClient<IDescriptionServicesAPI, DescriptionServicesAPI>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BestVIA.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Sử dụng Authorization để kích hoạt. \r\n\r\n 
                      Thêm 'Bearer' [space] và dòng token phía sau.
                      \r\n\r\Ví dụ: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                  new OpenApiSecurityScheme
                  {
                     Reference = new OpenApiReference
                     {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                     },
                     Scheme = "oauth2",
                     Name = "Bearer",
                     In = ParameterLocation.Header,

                  },
                   new List<string>()
                  }

                });

});

builder.Services.AddIdentity<IUsers, IdentityRole>(options =>
{
    //ko cần kí tự đặc biệt
    options.Password.RequireNonAlphanumeric = false;
    //Bắt buộc pass phải có chữ số
    options.Password.RequireDigit = true;
    //Phải có chữ thường và in hoa
    options.Password.RequireLowercase = true;
    //Độ dài tối thiểu là 6
    options.Password.RequiredLength = 6;

    //Số lần cao nhất để khóa
    options.Lockout.MaxFailedAccessAttempts = 5;

    //Thời gian tạm khóa
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);


}).AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();

//JWT TOKEN
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true
    };
    //SIGNALR
    //options.Authority = builder.Configuration["Authority:URL"]; // Cần cài đặt url mỗi lần chạy
    //options.Events = new JwtBearerEvents
    //{
    //    OnMessageReceived = context =>
    //    {
    //        var accessToken = context.Request.Query["access_token"];

    //        // If the request is for our hub...
    //        var path = context.HttpContext.Request.Path;
    //        if (!string.IsNullOrEmpty(accessToken) &&
    //            (path.StartsWithSegments("/hubs")))
    //        {
    //            // Read the token out of the query string
    //            context.Token = accessToken;
    //        }
    //        return Task.CompletedTask;
    //    }
    //};

});
//.Services.AddSignalR();


builder.Services.AddScoped<IAuthServicesAPI, AuthServicesAPI>();
builder.Services.AddScoped<IHistoryServicesAPI, HistoryServicesAPI>();


var app = builder.Build();
var uri = builder.Configuration["AuthSettings:Uri"];


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
  .WithOrigins(uri)
  .AllowAnyHeader()
  .AllowAnyMethod()
  .AllowCredentials());
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
