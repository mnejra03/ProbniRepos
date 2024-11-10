using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Helper;
using RSI_Pharmacy.Helper.Api;
using static RSI_Pharmacy.Endpoints.ProductEndpoints.ProductGetAllEndpoint;

namespace RSI_Pharmacy.Endpoints.ProductEndpoints;

//sa paging i sa filterom
[Route("products")]
public class ProductGetAllEndpoint(RSI_PharmacyContext db) : MyEndpointBaseAsync
    .WithRequest<ProductGetAllRequest>
    .WithResult<MyPagedList<ProductGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ProductGetAllResponse>> HandleAsync([FromQuery] ProductGetAllRequest request, CancellationToken cancellationToken = default)
    {
        // Kreiranje osnovnog query-a
        var query = db.Products
            .AsQueryable();

        // Primjena filtera na osnovu naziva proizvoda
        if (!string.IsNullOrWhiteSpace(request.FilterProductName))
        {
            query = query.Where(c => c.Name.Contains(request.FilterProductName));
        }

        // Primjena filtera na osnovu naziva kategorije
        if (!string.IsNullOrWhiteSpace(request.FilterCategoryName))
        {
            query = query.Where(c => c.Category != null && c.Category.Name.Contains(request.FilterCategoryName));
        }

        // Projektovanje rezultata u ProductGetAllResponse
        var projectedQuery = query.Select(p => new ProductGetAllResponse
        {
            ProductId = p.ProductId,
            Name = p.Name,
            CategoryName = p.Category != null ? p.Category.Name : ""
        });

        // Kreiranje paginiranog odgovora
        var result = await MyPagedList<ProductGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }
    public class ProductGetAllRequest : MyPagedRequest //naslijeđujemo
    {
        public string FilterProductName { get; set; } = string.Empty;
        public string FilterCategoryName { get; set; } = string.Empty;
    }

    public class ProductGetAllResponse
    {
        public required int ProductId { get; set; }
        public required string Name { get; set; }
        public required string CategoryName { get; set; }
    }
}