using Events.Models;

namespace Events.Repositories {
    public class EFPersonsRepository : IPersonsRepository {
        private EventsDbContext context;
        public EFPersonsRepository(EventsDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Person> Persons => context.Persons;
    }
}
