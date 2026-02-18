using Microsoft.EntityFrameworkCore;
using MyProject.Infrastructure.Database;
using MyProject.Domain.Aircrafts;
using MyProject.Domain.Airports;
using MyProject.Domain.Bookings;
using MyProject.Domain.Flights;
using MyProject.Domain.Passengers;
using MyProject.Domain.Seats;
using MyProject.Domain.Users;
using MyProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add db contexts 
builder.Services.AddDbContext<BookingSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// add scoped
builder.Services.AddScoped<IAircraftRepository, EfCoreAircraftRepository>();
builder.Services.AddScoped<IAirportRepository, EfCoreAirportRepository>();
builder.Services.AddScoped<IBookingRepository, EfCoreBookingRepository>();
builder.Services.AddScoped<IFlightRepository, EfCoreFlightRepository>();
builder.Services.AddScoped<IPassengerRepository, EfCorePassengerRepository>();
builder.Services.AddScoped<ISeatRepository, EfCoreSeatRepository>();
builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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
