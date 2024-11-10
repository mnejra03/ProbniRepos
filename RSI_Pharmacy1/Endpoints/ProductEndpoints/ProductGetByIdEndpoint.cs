using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Helper.Api;
using static RSI_Pharmacy.Endpoints.ProductEndpoints.ProductGetByIdEndpoint;

namespace RSI_Pharmacy.Endpoints.ProductEndpoints;

[Route("products")]
public class ProductGetByIdEndpoint(RSI_PharmacyContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithResult<ProductGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ProductGetByIdResponse> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await db.Products
                              .Where(p => p.ProductId == id)
                              .Select(p => new ProductGetByIdResponse
                              {
                                  ProductId = p.ProductId,
                                  Name = p.Name,
                                  Description = p.Description,
                                  Price = p.Price,
                                  QuantityInStock = p.QuantityInStock,
                                  ExpirationDate = p.ExpirationDate,
                                  CategoryId = p.CategoryId,
                                  CategoryName = p.Category != null ? p.Category.Name : null // Fetch Category name if exists
                              })
                              .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
            throw new KeyNotFoundException("Product not found");

        return product;
    }

    public class ProductGetByIdResponse
    {
        public required int ProductId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required double Price { get; set; }
        public required int QuantityInStock { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } // Assuming you want to include the Category name.
    }
}