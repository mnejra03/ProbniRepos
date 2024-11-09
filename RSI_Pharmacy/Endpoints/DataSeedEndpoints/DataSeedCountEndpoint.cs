namespace RSI_Pharmacy.Endpoints.DataSeed
{
    using Microsoft.AspNetCore.Mvc;
    using RSI_Pharmacy.Data;
    using RSI_Pharmacy.Helper.Api;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    namespace FIT_Api_Example.Endpoints
    {
        [Route("data-seed")]
        public class DataSeedCountEndpoint(RSI_PharmacyContext db)
            : MyEndpointBaseAsync
            .WithoutRequest
            .WithResult<Dictionary<string, int>>
        {
            [HttpGet]
            public override async Task<Dictionary<string, int>> HandleAsync(CancellationToken cancellationToken = default)
            {
                Dictionary<string, int> dataCounts = new ()
                {
                    { "Category", db.Category.Count() },
                    { "Product", db.Products.Count() },
                    { "MyAppUser", db.MyAppUsers.Count() }
                };

                return dataCounts;
            }
        }
    }

}
