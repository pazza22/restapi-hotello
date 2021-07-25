using HotelAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<HotelApiDBContext>());
        }
        public static async Task AddTestData(HotelApiDBContext dbContext)
        {
            if (dbContext.Rooms.Any())
            {
                return;
            }
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("12e7f1e2-2f0c-424c-93ca-834ce6ee7a34"),
                Name = "Majestic Mahal",
                Rate = 10119
            });
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("205dfe48-26f6-4753-8190-6342191dd207"),
                Name = "Residency Resorts",
                Rate = 23959
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
