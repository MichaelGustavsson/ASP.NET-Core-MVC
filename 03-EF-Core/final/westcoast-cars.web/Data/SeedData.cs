using System.Text.Json;
using westcoast_cars.web.Models;

namespace westcoast_cars.web.Data
{
    public static class SeedData
    {
        public static async Task LoadData(WestcoastCarsContext context)
        {
            // Steg 1.
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Steg 2. Vill endast ladda data om vår vehicles tabell är tom...
            if (context.Vehicles.Any()) return;

            // Steg 3. Läsa in json informationen ifrån vår vehicles.json fil...
            var json = System.IO.File.ReadAllText("Data/json/vehicles.json");

            // Steg 4. Omvandla json objekten till en lista av VehicleModel objekt...
            var vehicles = JsonSerializer.Deserialize<List<VehicleModel>>(json, options);

            // Steg 5. Skicka listan med VehicleModel objekt till databasen...
            if (vehicles is not null && vehicles.Count > 0)
            {
                await context.Vehicles.AddRangeAsync(vehicles);
                await context.SaveChangesAsync();
            }
        }
    }
}

/*
    dotnet ef database drop
    Radera hela Migration katalogen!!!
    dotnet ef migrations add InitialCreate -o Data/Migrations
    dotnet ef database update
*/