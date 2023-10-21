using events_service_data.Entities;
using EventsServiceData;
using Microsoft.EntityFrameworkCore;

namespace events_service_data.Repositories;

public class EventEntryRepository : IEventEntryRepository
{
    private readonly EventsServiceContext _context;
    public EventEntryRepository(EventsServiceContext context)
    {
        _context = context;
    }
    public async Task AddEventEntryAsync(EventEntry eventEntry)
    {
        await _context.EventEntries.AddAsync(eventEntry);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEventEntryAsync(EventEntry eventEntry)
    {
        _context.Remove(eventEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventEntry>> GetEntriesByEventId(int eventId)
    {
        return await _context.EventEntries
            .Where(ee => ee.UserId == eventId)
            .ToListAsync();
    }
}
