using Azure;
using Microsoft.AspNetCore.Mvc;
using RSI_Pharmacy.Helper.Api;
using RSI_Pharmacy.Services;
using System.Threading;
using System.Threading.Tasks;
using static RSI_Pharmacy.Endpoints.AuthEndpoints.AuthGetEndpoint;

namespace RSI_Pharmacy.Endpoints.AuthEndpoints
{
    [Route("auth")]
    public class AuthGetEndpoint(MyAuthService authService) : MyEndpointBaseAsync
        .WithoutRequest
        .WithActionResult<AuthGetResponse>
    {
        [HttpGet]
        public override async Task<ActionResult<AuthGetResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            // Retrieve user info based on the token
            var authInfo = authService.GetAuthInfo();

            if (!authInfo.IsLoggedIn)
            {
                return Unauthorized("Invalid or expired token");
            }

            // Return user information if the token is valid
            return Ok(new AuthGetResponse
            {
                MyAuthInfo = authInfo
            });
        }

        public class AuthGetResponse
        {
            public required MyAuthInfo MyAuthInfo { get; set; }
        }
    }
}
