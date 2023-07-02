using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpeedyWheels.Areas.Identity.Data;
using SpeedyWheels.Models;
using User = SpeedyWheels.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<RentalDataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("RentalDb"), b => b.MigrationsAssembly("SpeedyWheels")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) /*upraszczam se robote na razie zrobie potem bo tera mi sie nie chce*/
    .AddEntityFrameworkStores<RentalDataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
