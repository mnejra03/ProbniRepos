using Microsoft.AspNetCore.Mvc;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Data.Models;
using RSI_Pharmacy.Helper.Api;
using RSI_Pharmacy.Services;
using static RSI_Pharmacy.Endpoints.ProductEndpoints.ProductUpdateOrInsertEndpoint;

namespace RSI_Pharmacy.Endpoints.ProductEndpoints
{
    [Route("products")]
    public class ProductUpdateOrInsertEndpoint(RSI_PharmacyContext db, MyAuthService myAuthService) : MyEndpointBaseAsync
        .WithRequest<ProductUpdateOrInsertRequest>
        .WithActionResult<ProductUpdateOrInsertResponse>
    {
        [HttpPost]  // Using POST to support both create and update
        public override async Task<ActionResult<ProductUpdateOrInsertResponse>> HandleAsync([FromBody] ProductUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
        {

            MyAuthInfo myAuthInfo = myAuthService.GetAuthInfo();
            if (!myAuthInfo.IsLoggedIn)
            {
                return Unauthorized();
            }
            // Check if we're performing an insert or update based on the ID value
            bool isInsert = (request.ProductId == null || request.ProductId == 0);
            Product? product;

            if (isInsert)
            {
                // Insert operation: create a new product entity
                product = new Product();
                db.Products.Add(product); // Add the new product to the context
            }
            else
            {
                // Update operation: retrieve the existing product
                product = await db.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);

                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }
            }

            // Set common properties for both insert and update operations
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;

            // Save changes to the database
            await db.SaveChangesAsync(cancellationToken);

            return new ProductUpdateOrInsertResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId
            };
        }

        public class ProductUpdateOrInsertRequest
        {
            public int? ProductId { get; set; } // Nullable to allow null for insert operations
            public required string Name { get; set; }
            public required int CategoryId { get; set; }
        }

        public class ProductUpdateOrInsertResponse
        {
            public required int ProductId { get; set; }
            public required string Name { get; set; }
            public required int CategoryId { get; set; }
        }
    }
}
