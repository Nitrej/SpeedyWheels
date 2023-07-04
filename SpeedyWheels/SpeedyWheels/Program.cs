using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpeedyWheels.Areas.Identity.Data;
using SpeedyWheels.Models;
using User = SpeedyWheels.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<RentalDataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("RentalDb"), b => b.MigrationsAssembly("SpeedyWheels")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) /*upraszczam se robote na razie zrobie potem bo tera mi sie nie chce*/
    .AddEntityFrameworkStores<RentalDataContext>();


#region Authorization
AddAuthorizationPolicies(builder.Services);
#endregion

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

var serviceProvider = app.Services;

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "CarDetails",
    pattern: "{controller=CarDetails}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Cars",
    pattern: "{controller=Cars}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "RentsHistory",
    pattern: "{controller=RentsHistory}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "ClientOpinions",
    pattern: "{controller=ClientOpinions}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Clients",
    pattern: "{controller=Clients}/{action=Index}/{id?}");
app.MapRazorPages();

void AddAuthorizationPolicies(IServiceCollection services) {
    services.AddAuthorization(options => {
        options.AddPolicy("commonUserOnly", policy => policy.RequireClaim("commonUserNumber"));
    });
}


//var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();


//string[] roles = { "Admin", "Manager", "Member" };

//foreach (var role in roles) {
//    if (!await roleManager.RoleExistsAsync(role)) {
//        await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}

//var powerUser = new IdentityUser {

//    UserName = "poweruser@example.com",
//    Email = "poweruser@example.com"
//};
//await userManager.CreateAsync(powerUser, "Password123!");

//// Przypisanie roli admina do power usera
//await userManager.AddToRoleAsync(powerUser, "Admin");

app.Run();





