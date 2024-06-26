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
app.MapControllerRoute(
    name: "Invoices",
    pattern: "{controller=Invoices}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Services",
    pattern: "{controller=Services}/{action=Index}/{id?}");
app.MapRazorPages();

void AddAuthorizationPolicies(IServiceCollection services) {
    services.AddAuthorization(options => {
        options.AddPolicy("commonUserOnly", policy => policy.RequireClaim("commonUser"));
        options.AddPolicy("operatorOnly", policy => policy.RequireClaim("Operator"));
        options.AddPolicy("administratorOnly", policy => policy.RequireClaim("Administrator"));
        options.AddPolicy("moderatorsOnly", policy => policy.RequireAssertion(context => {
            bool isAdmin = context.User.HasClaim("Administrator", "true");
            bool isOperator = context.User.HasClaim("Operator", "true");
            return isAdmin || isOperator;
        }));
    });
}




app.Run();





