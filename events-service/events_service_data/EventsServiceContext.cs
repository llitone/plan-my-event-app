using events_service_data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsServiceData;

public class EventsServiceContext : DbContext
{
    public EventsServiceContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<EventEntry> EventEntries { get; set; }
    public DbSet<FavouriteEvent> FavoriteEvents { get; set; }
}
