using UserManagementAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapHelloWorld();

app.Run();