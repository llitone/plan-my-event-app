using events_service.Dtos;
using events_service.Model;
using events_service.Services;
using events_service_data.Entities;
using events_service_data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace events_service.Controllers;

[ApiController]
[Route("events-service/api/v1/entries/")]
public class EventEntryController : ControllerBase
{
    private readonly IEventEntryRepository _eventEntryRepository;
    private readonly TokenDecoder _tokenDecoder;

    public EventEntryController(IEventEntryRepository eventEntryRepository,
                                TokenDecoder tokenDecoder)
    {
        _eventEntryRepository = eventEntryRepository;
        _tokenDecoder = tokenDecoder;
    }

    [HttpGet, Route("signup/{eventId}")]
    public async Task<IActionResult> EventEntry(int eventId)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }

        if(await _eventEntryRepository.IsEntryAsync(eventId))
        {
            return BadRequest();
        }

        await _eventEntryRepository.AddEventEntryAsync(new EventEntry()
        {
            EventId = eventId,
            SignedAt = DateTime.UtcNow,
            UserId = user.Id,
        });

        return Ok();
    }

    [HttpGet, Route("all/{eventId}")]
    public async Task<IActionResult> GetAllEntries(int eventId)
    {
        var result = (await _eventEntryRepository.GetEntriesByEventId(eventId)).Select(ee => new ReadEventEntryDto()
        {
            EventId = eventId,
            SignedAt = ee.SignedAt,
            UserId = ee.UserId
        });

        return new JsonResult(result);
    }

    [HttpGet, Route("cancel/{eventId}")]
    public async Task<IActionResult> CancelEventEntry(int eventId)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }

        if (await _eventEntryRepository.IsEntryAsync(eventId) == false)
        {
            return BadRequest();
        }

        await _eventEntryRepository.DeleteEventEntryAsync(user.Id, eventId);

        return Ok();
    }
}
