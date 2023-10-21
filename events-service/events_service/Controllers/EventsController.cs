using Microsoft.AspNetCore.Mvc;
using events_service_data.Repositories;
using events_service.Dtos;
using EventsServiceData;
using events_service_data.Entities;
using System.Text;
using events_service.Services;
using events_service.Model;

namespace events_service.Controllers;

[ApiController]
[Route("events-service/api/v1/events/")]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _eventsRepository;
    private readonly TokenDecoder _tokenDecoder;

    public EventsController(IEventRepository eventsRepository,
                            TokenDecoder tokenDecoder)
    {
        _eventsRepository = eventsRepository;
        _tokenDecoder = tokenDecoder;
    }

    [HttpGet, Route("all")]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = (await _eventsRepository.GetAllEventsAsync())
            .Select(e => new ReadEventDto()
        {
            Id = e.Id,
            StartAt = e.StartAt,
            Address = e.Address,
            Description = e.Description,
            Category = new CategoryDto()
            {
                Id= e.Category.Id,
                Name = e.Category.Name,
            },
            CreatedAt = e.CreatedAt,
            CreatorId = e.CreatorId,
            Name = e.Name,
            Price = e.Price
        });
        return new JsonResult(result);
    }

    [HttpGet, Route("all/category/{categoryId}")]
    public async Task<IActionResult> GetAllEventsByCategory(int categoryId)
    {
        var result = (await _eventsRepository.GetEventsByCategoryAsync(new Category() { Id = categoryId }))
            .Select(e => new ReadEventDto()
            {
                Id = e.Id,
                StartAt = e.StartAt,
                Address = e.Address,
                Description = e.Description,
                Category = new CategoryDto()
                {
                    Id = e.Category.Id,
                    Name = e.Category.Name,
                },
                CreatedAt = e.CreatedAt,
                CreatorId = e.CreatorId,
                Name = e.Name,
                Price = e.Price
            });
        return new JsonResult(result);
    }

    [HttpGet, Route("all/date/before")]
    public async Task<IActionResult> GetAllEventsByDateBefore(DateTime date)
    {
        var result = (await _eventsRepository.GetEventsByDateBeforeAsync(date))
            .Select(e => new ReadEventDto()
            {
                Id = e.Id,
                StartAt = e.StartAt,
                Address = e.Address,
                Description = e.Description,
                Category = new CategoryDto()
                {
                    Id = e.Category.Id,
                    Name = e.Category.Name,
                },
                CreatedAt = e.CreatedAt,
                CreatorId = e.CreatorId,
                Name = e.Name,
                Price = e.Price
            });
        return new JsonResult(result);
    }

    [HttpGet, Route("all/date/after")]
    public async Task<IActionResult> GetAllEventsByDateAfter(DateTime date)
    {
        var result = (await _eventsRepository.GetEventsByDateAfterAsync(date))
            .Select(e => new ReadEventDto()
            {
                Id = e.Id,
                StartAt = e.StartAt,
                Address = e.Address,
                Description = e.Description,
                Category = new CategoryDto()
                {
                    Id = e.Category.Id,
                    Name = e.Category.Name,
                },
                CreatedAt = e.CreatedAt,
                CreatorId = e.CreatorId,
                Name = e.Name,
                Price = e.Price
            });
        return new JsonResult(result);
    }

    [HttpGet, Route("event/{eventId}")]
    public async Task<IActionResult> GetEventById(int eventId)
    {
        var bufEvent = await _eventsRepository.GetEventByIdAsync(eventId);

        if(bufEvent is null)
        {
            return new JsonResult(null);
        }

        ReadEventDto result = new()
        {
            Id = bufEvent.Id,
            StartAt = bufEvent.StartAt,
            Address = bufEvent.Address,
            Description = bufEvent.Description,
            Category = new CategoryDto()
            {
                Id = bufEvent.Category.Id,
                Name = bufEvent.Category.Name,
            },
            CreatedAt = bufEvent.CreatedAt,
            CreatorId = bufEvent.CreatorId,
            Name = bufEvent.Name,
            Price = bufEvent.Price
        };

        return new JsonResult(result);
    }

    [HttpPost, Route("event/")]
    public async Task<IActionResult> AddEvent([FromBody] CreateEventDto eventDto)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if(user is null)
        {
            return Unauthorized();
        }

        int id = await _eventsRepository.AddEventAsync(new Event()
        {
            Address = eventDto.Address,
            Name = eventDto.Name,
            Description = eventDto.Description,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            StartAt = eventDto.StartAt,
            Price = eventDto.Price,
            CategoryId = eventDto.Category.Id,
            CreatorId = user.Id
        });

        return Ok(id);
    }

    [HttpPost, Route("event/{eventId}")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }
        Event? e = await _eventsRepository.GetEventByIdAsync(eventId);
        if (e is null)
        {
            return NotFound();
        }
        if(user.Id != e.CreatorId)
        {
            return Forbid();
        }

        await _eventsRepository.DeleteEventAsync(eventId);

        return Ok();
    }
}
