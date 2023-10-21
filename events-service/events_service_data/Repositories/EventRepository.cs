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
    public async Task<int> AddEventAsync(Event e)
    {
        var addedEvent = await _context.Events.AddAsync(e);
        await _context.SaveChangesAsync();
        return addedEvent.Entity.Id;
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
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetEventsByCategoryAsync(Category category)
    {
        return await _context.Events
            .Where(e => e.Category.Id == category.Id)
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByDateAfterAsync(DateTime date)
    {
        return await _context.Events
            .Where(e => e.StartAt > date)
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByDateBeforeAsync(DateTime date)
    {
        return await _context.Events
            .Where(e => e.StartAt <= date)
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetUserFavouritesEventsAsync(int userId)
    {
        return await _context.FavoriteEvents
            .Where(fe => fe.UserId == userId)
            .Select(fe => fe.Event)
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task UpdateEventAsync(Event e)
    {
        _context.Events.Update(e);
        await _context.SaveChangesAsync();
    }
}
