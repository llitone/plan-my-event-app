using events_service_data.Entities;

namespace events_service_data.Repositories;

public interface IEventEntryRepository
{
    Task AddEventEntryAsync(EventEntry eventEntry);
    Task DeleteEventEntryAsync(EventEntry eventEntry);
    Task<IEnumerable<EventEntry>> GetEntriesByEventId(int eventId);
}
