using files_service_data.Repositories;
using System.Security.Cryptography;

namespace files_service.Services;

public class ImageSaver : IImageSaver
{
    private readonly IImageRepository _imageRepository;
    public ImageSaver(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task SaveImageAsync(string image, int eventId)
    {
        MD5 md5 = MD5.Create();
        byte[] imageBytes = Convert.FromBase64String(image);
        byte[] hashBytes = md5.ComputeHash(imageBytes);
        string hash = Convert.ToHexString(hashBytes);

        var dir = "./images/" + hash.Substring(0, 3) + "/" + hash.Substring(3, 3);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string filename = dir + "/" + Guid.NewGuid() + ".jpg";
        using (FileStream file = File.Create(filename))
        {
            await file.WriteAsync(imageBytes, 0, imageBytes.Length);
        }

        await _imageRepository.AddImageAsync(filename, eventId);
    }
}
