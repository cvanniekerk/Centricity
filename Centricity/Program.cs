using Centricity.Data;
using Centricity.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CentricityContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CentricityContext") ?? throw new InvalidOperationException("Connection string 'CentricityContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// remove cyclic dependancy error when creating via api
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Postgres timestamps
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// seed data
using (var scope = app.Services.CreateScope())
{    
    SeedData.Initialize(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// swagger
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
