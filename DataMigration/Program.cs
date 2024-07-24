using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataMigration.Data;
using TargetDDContext.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataMigrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataMigrationContext") ?? throw new InvalidOperationException("Connection string 'DataMigrationContext' not found.")));

builder.Services.AddDbContext<TargetDDDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TargetDDDbContext") ?? throw new InvalidOperationException("Connection string 'TargetDDDbContext' not found.")));



// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
