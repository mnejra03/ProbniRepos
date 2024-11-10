namespace RSI_Pharmacy.Endpoints.ProductEndpoints;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Data.Models;
using RSI_Pharmacy.Helper.Api;
using RSI_Pharmacy.Services;
using System.Threading;
using System.Threading.Tasks;

[MyAuthorization(isAdmin: true, isManager : false)]
[Route("products")]
public class ProductDeleteEndpoint(RSI_PharmacyContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{

    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await db.Products.SingleOrDefaultAsync(x => x.ProductId == id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException("Product not found");

        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);
    }
}

