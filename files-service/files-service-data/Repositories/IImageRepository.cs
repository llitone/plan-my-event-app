namespace files_service_data.Repositories;

public interface IImageRepository
{
    Task DeleteImageByEventIdAsync(int eventId);
    Task DeleteImageByIdAsync(int id);
    Task AddImageAsync(string path, int eventId);
}
