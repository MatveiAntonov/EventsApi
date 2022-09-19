using Events.Models;

namespace Events.Repositories {
    public interface IPersonsRepository {
        IQueryable<Person> Persons { get; }
    }
}
