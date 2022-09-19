using Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.App_Start {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            EventsDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<EventsDbContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Events.Any()) {
                context.Events.AddRange(
                    new Event {
                        Theme = "Corporate",
                        Description = "New-Year corporate 2023",
                        Organizer = "Syberry management",
                        Time = new DateTime(2023, 12, 31, 19, 0, 0),
                        Place = "Ski resort \"Silichi\""
                    },
                    new Event {
                        Theme = "English practise",
                        Description = "Training employees in spoken English",
                        Organizer = "Syberry management",
                        Speaker = "English teacher Natalya Kaskar",
                        Time = new DateTime(2022, 9, 20, 12, 0, 0),
                        Place = "Modsen office"
                    },
                    new Event {
                        Theme = "Fifa Tournament",
                        Description = "Playstation 5 Fifa 2022 competition",
                        Organizer = "Syberry management",
                        Time = new DateTime(2022, 10, 15, 18, 0, 0),
                        Place = "Modsen office"
                    }
                    );
            }
            if (!context.Persons.Any()) {
                context.Persons.AddRange(
                    new Person {
                        Login = "user",
                        Password = "12345"
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}
