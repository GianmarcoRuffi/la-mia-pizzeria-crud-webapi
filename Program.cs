using la_mia_pizzeria_static.Models.Repositories;
using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PizzaDbContextConnection");builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PizzaDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

//da inserirsi sotto a AddControllersWithViews
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// fix
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

//

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";

System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


//

//injections per far partire con db
//builder.Services.AddScoped<IPizzeriaRepository, DbPizzeriaRepository>();

//

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

// per la parte di Identity
app.UseAuthentication();

app.UseAuthorization();

// per la parte di Identity
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=Index}/{id?}");
app.Run();


