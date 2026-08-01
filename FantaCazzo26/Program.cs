using FantaCazzo26.Data;
using FantaCazzo26.Services;
using FantaCazzo26.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Entity Framework + MySQL
builder.Services.AddDbContext<FantaCazzo26Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )));

// Dependency Injection
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<ISquadsService, SquadsService>();
builder.Services.AddScoped<IAcquisitionService, AcquisitionService>();

// Swagger (utile per testare le API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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