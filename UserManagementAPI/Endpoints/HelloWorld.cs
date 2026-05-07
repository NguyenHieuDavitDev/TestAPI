namespace UserManagementAPI.Endpoints;

public static class HelloWorld
{
    public static void MapHelloWorld(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World from HelloWorld endpoint! Xin chào các bạn!");
        app.MapGet("/an", () => "Xin chào");
        
    }
}