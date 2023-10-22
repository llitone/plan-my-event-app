using Microsoft.EntityFrameworkCore;

namespace files_service_data.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly FilesServiceContext _context;
    public ImageRepository(FilesServiceContext context)
    {
        _context = context;
    }
    public async Task AddImageAsync(string path, int eventId)
    {
        await _context.Images.AddAsync(new Entities.Image()
        {
            EventId = eventId,
            Path = path
        });
        await _context.SaveChangesAsync();
    }

    public async Task DeleteImageByEventIdAsync(int eventId)
    {
        Entities.Image? image = await _context.Images
            .FirstOrDefaultAsync(img => img.EventId == eventId);

        if(image != null)
        {
            _context.Remove(image);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteImageByIdAsync(int id)
    {
        _context.Images.Remove(new Entities.Image() { Id = id });
        await _context.SaveChangesAsync();
    }
}
