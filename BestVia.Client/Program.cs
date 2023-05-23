using BestVia.Client;
using BestVia.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System;
using BestVia.Client.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });




//Add Radzen
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddTransient<RequestHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSingleton<IFileServicesClient, FileServicesClient>();

builder.Services.AddBlazorDownloadFile();


var uri = builder.Configuration["Config:Uri"];
var agent = builder.Configuration["Config:Agent"];


builder.Services.AddHttpClient<IUserServicesClient, UserServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient<IDescriptionServicesClient, DescriptionServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient<IKeyServicesClient, KeyServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient<IProxyServicesClient, ProxyServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient<IOrderServicesClient, OrderServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient<ISummaryServicesClient, SummaryServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddHttpClient<IViaServicesClient, ViaServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddHttpClient<ICategoryServicesClient, CategoryServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddHttpClient<IProductServicesClient, ProductServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddHttpClient<IProductTypeServicesClient, ProductTypeServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddHttpClient<IAuthServicesClient, AuthServicesClient>(
client =>
{

    client.BaseAddress = new Uri(uri);
    client.DefaultRequestHeaders.UserAgent.ParseAdd(agent);
}).AddHttpMessageHandler<RequestHandler>();


builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

var app = builder.Build();

