using Microsoft.EntityFrameworkCore;
using Pustok2.DAL;
using Pustok2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Pustok2DbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<LayoutService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapControllerRoute(
    name:"areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();
