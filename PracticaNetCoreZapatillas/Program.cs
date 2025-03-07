using Microsoft.EntityFrameworkCore;
using PracticaNetCoreZapatillas.Data;
using PracticaNetCoreZapatillas.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string cs = builder.Configuration.GetConnectionString("SqlZapas");

builder.Services.AddTransient<RepositoryZapatillas>();

builder.Services.AddDbContext<ZapasContext>(x => x.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
