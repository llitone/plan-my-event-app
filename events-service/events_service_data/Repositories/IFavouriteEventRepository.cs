using events_service_data.Entities;

namespace events_service_data.Repositories;

public interface IFavouriteEventRepository
{
    Task AddFavouriteEventAsync(FavouriteEvent favouriteEvent);
    Task DeleteFavouriteEventAsync(int userId, int eventId);
    Task<IEnumerable<Event>> GetFavouriteEventsAsync(int userId);
    Task<bool> IsFavourite(int userId, int eventId);
}
