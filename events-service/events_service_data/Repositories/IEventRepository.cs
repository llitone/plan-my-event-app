using events_service_data.Entities;

namespace events_service_data.Repositories;

public interface IEventRepository
{
    Task<int> AddEventAsync(Event e);
    Task DeleteEventAsync(int id);
    Task<IEnumerable<Event>> GetEventsByCategoryAsync(Category category);
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<IEnumerable<Event>> GetEventsByDateBeforeAsync(DateTime date);
    Task<IEnumerable<Event>> GetEventsByDateAfterAsync(DateTime date);
    Task<Event?> GetEventByIdAsync(int id);
    Task UpdateEventAsync(Event e);
    Task<IEnumerable<Event>> GetUserFavouritesEventsAsync(int userId);
}
