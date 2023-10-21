using events_service_data.Entities;
using EventsServiceData;
using Microsoft.EntityFrameworkCore;

namespace events_service_data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EventsServiceContext _context;

    public CategoryRepository(EventsServiceContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}
