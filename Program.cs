using DynamicData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieContext>
//(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
var app = builder.Build();
// Seed işlemi development ortamında çalışsın
//if (app.Environment.IsDevelopment())
//{
//    DataSeeding.Seed(app);
//}
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
