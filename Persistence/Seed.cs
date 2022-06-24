using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if(context.Activities.Any())
            {
                return;
            }

            var activities = new List<Activity>{
                new Activity{
                    Title = "Activity 1",
                    Date = DateTime.Now.AddDays(-2),
                    Description = "Past Activity 1",
                    Category = "Fun",
                    City = "NY",
                    Venue = "Pub"
                },
                new Activity{
                    Title = "Activity 2",
                    Date = DateTime.Now.AddDays(-1),
                    Description = "Past Activity 2",
                    Category = "Fun",
                    City = "SH",
                    Venue = "Pub"
                },
                new Activity{
                    Title = "Activity 3",
                    Date = DateTime.Now.AddDays(1),
                    Description = "Coming Activity 2",
                    Category = "Fun",
                    City = "SH",
                    Venue = "Pub"
                },
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();

        }
    }
}