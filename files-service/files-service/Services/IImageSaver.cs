namespace files_service.Services;

public interface IImageSaver
{
    Task SaveImageAsync(string image, int eventId);
}
