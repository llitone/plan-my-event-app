using events_service_data.Entities;
using EventsServiceData;
using Microsoft.EntityFrameworkCore;

namespace events_service_data.Repositories;

public class FavouriteEventRepository : IFavouriteEventRepository
{
    private readonly EventsServiceContext _context;

    public FavouriteEventRepository(EventsServiceContext context)
    {
        _context = context;
    }
    public async Task AddFavouriteEventAsync(FavouriteEvent favouriteEvent)
    {
        await _context.FavoriteEvents.AddAsync(favouriteEvent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFavouriteEventAsync(int userId, int eventId)
    {
        FavouriteEvent? favouriteEvent = await _context.FavoriteEvents
            .FirstOrDefaultAsync(e => e.UserId == userId && e.EventId == eventId);

        if (favouriteEvent != null) 
        {
            _context.FavoriteEvents.Remove(favouriteEvent);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Event>> GetFavouriteEventsAsync(int userId)
    {
        return await _context.FavoriteEvents
            .Where(fe => fe.UserId == userId)
            .Select(fe => fe.Event).ToListAsync();
    }

    public async Task<bool> IsFavourite(int userId, int eventId)
    {
        return (await _context.FavoriteEvents
            .FirstOrDefaultAsync(fe => fe.UserId == userId && fe.EventId == eventId)) != null;
    }
}
