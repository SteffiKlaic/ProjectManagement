using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projektverwaltung.Data;
using Projektverwaltung.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProjektverwaltungContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektverwaltungContext") ?? throw new InvalidOperationException("Connection string 'ProjektverwaltungContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ProjektverwaltungContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
