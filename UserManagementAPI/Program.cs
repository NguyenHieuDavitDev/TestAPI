using UserManagementAPI.Endpoints;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// đăng ký DbContext trước
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// build sau khi đã add service
var app = builder.Build();

// API
app.MapHelloWorld();
app.MapProductEndpoints();

app.Run();