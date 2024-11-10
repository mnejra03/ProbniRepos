using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Helper.Api;
using static RSI_Pharmacy.Endpoints.CategoryEndpoints.CategoryGetAllEndpoint;

namespace RSI_Pharmacy.Endpoints.CategoryEndpoints;

// Endpoint bez paging-a i bez filtera
[Route("categories")]
public class CategoryGetAllEndpoint(RSI_PharmacyContext db) : MyEndpointBaseAsync
    .WithoutRequest
    .WithResult<CategoryGetAllResponse[]>
{
    [HttpGet("all")]
    public override async Task<CategoryGetAllResponse[]> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await db.Category
                        .Select(c => new CategoryGetAllResponse
                        {
                            CategoryId = c.CategoryId,
                            Name = c.Name
                        })
                        .ToArrayAsync(cancellationToken);

        return result;
    }

    public class CategoryGetAllResponse
    {
        public required int CategoryId { get; set; }
        public required string Name { get; set; }
    }
}
