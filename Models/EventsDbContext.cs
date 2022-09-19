using Microsoft.EntityFrameworkCore;

namespace Events.Models {
    public class EventsDbContext : DbContext{
        public EventsDbContext(DbContextOptions<EventsDbContext> options) 
            : base(options) { }
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Person> Persons => Set<Person>();
    }
}
