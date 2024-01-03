using ECommerceFPE.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using ECommerceFPE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ECommerceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceFPEContext") ?? throw new InvalidOperationException("Connection string 'ECommerceFPEContext' not found.")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ECommerceDBContext>()
    .AddDefaultTokenProviders();


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

app.UseAuthentication();
app.UseAuthorization();


/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

*/


//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");




//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});









app.Run();
