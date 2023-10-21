using events_service_data.Entities;
using EventsServiceData;
using Microsoft.EntityFrameworkCore;

namespace events_service_data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly EventsServiceContext _context;
    public EventRepository(EventsServiceContext context)
    {
        _context = context;
    }
    public async Task AddEventAsync(Event e)
    {
        await _context.Events.AddAsync(e);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        var deletedEvent = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id);

        if(deletedEvent != null)
        {
            deletedEvent.IsDeleted = true;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _context.Events
            .Where(e => e.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetEventsByCategoryAsync(Category category)
    {
        return await _context.Events
            .Where(e => e.CategoryId == category.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByDateAfterAsync(DateTime date)
    {
        return await _context.Events
            .Where(e => e.StartAt > date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByDateBeforeAsync(DateTime date)
    {
        return await _context.Events
            .Where(e => e.StartAt <= date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetUserFavouritesEventsAsync(int userId)
    {
        return await _context.FavoriteEvents
            .Where(fe => fe.UserId == userId)
            .Select(fe => fe.Event)
            .ToListAsync();
    }

    public async Task UpdateEventAsync(Event e)
    {
        _context.Events.Update(e);
        await _context.SaveChangesAsync();
    }
}
