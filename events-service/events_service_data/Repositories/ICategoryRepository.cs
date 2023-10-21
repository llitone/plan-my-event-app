using events_service_data.Entities;

namespace events_service_data.Repositories;

public interface ICategoryRepository 
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}
