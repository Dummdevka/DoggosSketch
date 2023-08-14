using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DAL;
using Serilog;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.addDAL();

//Logging
Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.ReadFrom.Configuration(builder.Configuration)
	.WriteTo.File("logs/log-.txt")
	.CreateLogger();

//Caching
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
	options.InstanceName = "doggossketch_";
});
builder.Services.addServices();

builder.Logging.AddSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

