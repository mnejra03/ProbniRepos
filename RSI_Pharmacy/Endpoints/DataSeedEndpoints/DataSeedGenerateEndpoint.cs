namespace RSI_Pharmacy.Endpoints.DataSeed;

using Microsoft.AspNetCore.Mvc;
using RSI_Pharmacy.Data;
using RSI_Pharmacy.Data.Models;
using RSI_Pharmacy.Data.Models.Auth;
using RSI_Pharmacy.Helper.Api;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[Route("data-seed")]
public class DataSeedGenerateEndpoint(RSI_PharmacyContext db)
    : MyEndpointBaseAsync
    .WithoutRequest
    .WithResult<string>
{
    [HttpPost]
    public override async Task<string> HandleAsync(CancellationToken cancellationToken = default)
    {
        // Kreiranje kategorija
        var categories = new List<Category>
        {
            new Category { Name = "Medications" },
            new Category { Name = "Supplements and vitamins" },
            new Category { Name = "Cosmetics and personal care" },
            new Category { Name = "Medical equipement and aids" },
            new Category { Name = "Baby and maternity products" }
        };

        // Kreiranje proizvoda
        var products = new List<Product>
        {
            new Product { Name = "Paracetamol", Category = categories[0] },
            new Product { Name = "Antibiotics", Category = categories[0] },

            new Product { Name = "Centrum", Category = categories[1] },
            new Product { Name = "Magnesium", Category = categories[1] },

            new Product { Name = "Head and shoulders shampoo", Category = categories[2] },
            new Product { Name = "Oral B electric toothbrush", Category = categories[2] },

            new Product { Name = "Elastic knee brace", Category = categories[3] },
            new Product { Name = "Omron inhaler", Category = categories[3] },

            new Product { Name = "Aveeno baby cream", Category = categories[4] },
            new Product { Name = "Pampers", Category = categories[4] }
        };

        // Kreiranje korisnika s ulogama
        var users = new List<MyAppUser>
        {
            new MyAppUser
            {
                Username = "admin1",
                Password = "admin123",
                FirstName = "Admin",
                LastName = "One",
                IsAdmin = true, 
                IsManager = false 
            },
            new MyAppUser
            {
                Username = "manager1",
                Password = "manager123",
                FirstName = "Manager",
                LastName = "One",
                IsAdmin = false,
                IsManager = true 
            },
            new MyAppUser
            {
                Username = "user1",
                Password = "user123",
                FirstName = "User",
                LastName = "One",
                IsAdmin = false, 
                IsManager = false 
            },
            new MyAppUser
            {
                Username = "user2",
                Password = "user456",
                FirstName = "User",
                LastName = "Two",
                IsAdmin = false,
                IsManager = false
            }
        };

        // Dodavanje podataka u bazu
        await db.Category.AddRangeAsync(categories, cancellationToken);
        await db.Products.AddRangeAsync(products, cancellationToken);
        await db.MyAppUsers.AddRangeAsync(users, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return "Data generation completed successfully.";
    }
}
