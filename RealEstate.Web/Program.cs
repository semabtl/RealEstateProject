using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//HttpContext.Session yerine kullaným için
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<RealEstateContext>(options => options.UseSqlServer("Server=.;Database=RealEstate;Trusted_Connection=True;"));

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAddNewAdvertService, AddNewAdvertService>();
builder.Services.AddScoped<IContactApplicationService, ContactApplicationService>();
builder.Services.AddScoped<IListAdvertsService, ListAdvertsService>();
builder.Services.AddScoped<IAllCitiesService, AllCitiesService>();
builder.Services.AddScoped<IAdvertDetailsService, AdvertDetailsService>();
builder.Services.AddScoped<IPaidAdvertsHomepageService, PaidAdvertsHomepageService>();
builder.Services.AddScoped<IFavouritesService, FavouritesService>();

// Configure session
builder.Services.AddDistributedMemoryCache(); // Session için önbellek kullanýmý
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HttpOnly
    options.Cookie.IsEssential = true; // Session çerezi gerekli
});

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
