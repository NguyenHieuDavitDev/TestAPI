using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", async (AppDbContext db) =>
            await db.Products.ToListAsync());

        app.MapGet("/products/{id}", async (Guid id, AppDbContext db) =>
            await db.Products.FindAsync(id) is Product p ? Results.Ok(p) : Results.NotFound());

        app.MapPost("/products", async (Product product, AppDbContext db) =>
        {
            product.Id = Guid.NewGuid();
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        app.MapPut("/products/{id}", async (Guid id, Product input, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            product.Name = input.Name;
            product.Price = input.Price;
            product.Quantity = input.Quantity;

            await db.SaveChangesAsync();
            return Results.Ok(product);
        });

        app.MapDelete("/products/{id}", async (Guid id, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}