using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Helper.Api;
using static RSI_Pharmacy.Endpoints.CityEndpoints.CityGetByIdEndpoint;

namespace RSI_Pharmacy.Endpoints.CityEndpoints;

[Route("products")]
public class CityGetByIdEndpoint(RSI_PharmacyContext db) : MyEndpointBaseAsync
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
                                CategoryId = p.CategoryId
                            })
                            .FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException("Product not found");

        return product;
    }

    public class ProductGetByIdResponse
    {
        public required int ProductId { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
    }
}
