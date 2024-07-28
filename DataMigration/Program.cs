using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataMigration.Data;
using TargetDDContext.Data;
using SourceDDContext.Data;
using Sakura.AspNetCore.Mvc;
using DataMigration.Services.KitchenSink;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataMigrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataMigrationContext") ?? throw new InvalidOperationException("Connection string 'DataMigrationContext' not found.")));

builder.Services.AddDbContext<TargetDDDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TargetDDDbContext") ?? throw new InvalidOperationException("Connection string 'TargetDDDbContext' not found.")));


builder.Services.AddDbContext<SourceDDDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SourceDDDbContext") ?? throw new InvalidOperationException("Connection string 'SourceDDDbContext' not found.")));



// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    
//;

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromDays(7);
});

//Allows for Dependency Injection of IHttpContext
builder.Services.AddHttpContextAccessor();

//Required for Sakura TagHelper
builder.Services.AddBootstrapPagerGenerator(options =>
{
    // Use default pager options.
    options.ConfigureDefault();
});

builder.Services.AddTransient<IKitchenSink, KitchenSink>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
