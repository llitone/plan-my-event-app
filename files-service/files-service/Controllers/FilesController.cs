using files_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace files_service.Controllers;

[ApiController]
[Route("files-service/api/v1/")]
public class FilesController : ControllerBase
{
    private readonly IImageSaver _imageSaver;
    public FilesController(IImageSaver imageSaver)
    {
        _imageSaver = imageSaver;
    }

    [HttpPost, Route("upload/{eventId}")]
    public async Task<IActionResult> Upload([FromBody]string base64File, int eventId)
    {
        try
        {
            await _imageSaver.SaveImageAsync(base64File, eventId);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet, Route("download/{eventId}")]
    public async Task<IActionResult> Upload(int eventId)
    {
        
    }
}
