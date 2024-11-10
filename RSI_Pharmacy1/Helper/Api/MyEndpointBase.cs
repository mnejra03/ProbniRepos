using Microsoft.AspNetCore.Mvc;

namespace RSI_Pharmacy.Helper.Api;

//https://github.com/ardalis/ApiEndpoints/blob/main/src/Ardalis.ApiEndpoints/EndpointBase.cs
/// <summary>
/// A base class for an API controller with single action (endpoint).
/// </summary>
[ApiController]
public abstract class MyEndpointBase : ControllerBase
{
}