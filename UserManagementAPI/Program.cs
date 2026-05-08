using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using ProductAPI.Data;
using ProductAPI.Modules.Product.Repositories;
using ProductAPI.Modules.Product.Repositories.Interfaces;
using ProductAPI.Modules.Product.Services;
using ProductAPI.Modules.Product.Services.Interfaces;
using System.Globalization;

LoadDotEnv();

var builder = WebApplication.CreateBuilder(args);

// add controller
builder.Services.AddControllers();

// add localization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US") };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
// connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

// add repository
builder.Services.AddScoped<
    IProductRepository,
    ProductRepository>();

// add service
builder.Services.AddScoped<
    IProductService,
    ProductService>();

// add endpoint api explorer
builder.Services.AddEndpointsApiExplorer();

// add swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// use localization
app.UseRequestLocalization();

// use swagger
app.UseSwagger();

// use swagger ui
app.UseSwaggerUI();

// use static files
app.UseStaticFiles();

// use https redirection
app.UseHttpsRedirection();

// use authorization
app.UseAuthorization();

// map controllers
app.MapControllers();

// run the application
app.Run();

// load .env file
static void LoadDotEnv()
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (!File.Exists(envPath))
    {
        return;
    }

    foreach (var rawLine in File.ReadAllLines(envPath))
    {
        var line = rawLine.Trim();

        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
        {
            continue;
        }

        var separatorIndex = line.IndexOf('=');
        if (separatorIndex <= 0)
        {
            continue;
        }

        var key = line[..separatorIndex].Trim();
        var value = line[(separatorIndex + 1)..].Trim();
        Environment.SetEnvironmentVariable(key, value);
    }
}