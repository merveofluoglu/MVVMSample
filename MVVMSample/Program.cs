using Microsoft.EntityFrameworkCore;
using MVVMDataLayer.Context;
using MVVMDataLayer.RepositoryClasses;
using MVVMDataLayer.RepositoryInterfaces;
using MVVMViewModelLayer;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build(); ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:conStr"]));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookViewModel>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
