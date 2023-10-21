using events_service.Dtos;
using events_service.Model;
using events_service.Services;
using events_service_data.Entities;
using events_service_data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace events_service.Controllers;

[ApiController]
[Route("events-service/api/v1/favourite/")]
public class FavouritesEventsController : ControllerBase
{
    private readonly TokenDecoder _tokenDecoder;
    private readonly IFavouriteEventRepository _favouriteEventRepository;

    public FavouritesEventsController(TokenDecoder tokenDecoder,
                                      IFavouriteEventRepository favouriteEventRepository)
    {
        _tokenDecoder = tokenDecoder;
        _favouriteEventRepository = favouriteEventRepository;
    }

    [HttpGet, Route("all")]
    public async Task<IActionResult> GetFavouritesEvents()
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }

        var result = (await _favouriteEventRepository.GetFavouriteEventsAsync(user.Id)).Select(e => new ReadEventDto()
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

    [HttpGet, Route("add/{eventId}")]
    public async Task<IActionResult> AddFavouriteEvent(int eventId)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }

        if (await _favouriteEventRepository.IsFavourite(user.Id, eventId))
        {
            return BadRequest();
        }

        await _favouriteEventRepository.AddFavouriteEventAsync(new FavouriteEvent()
        {
            EventId = eventId,
            UserId = user.Id,
        });

        return Ok();
    }

    [HttpGet, Route("remove/{eventId}")]
    public async Task<IActionResult> RemoveFavouriteEvent(int eventId)
    {
        UserModel? user = _tokenDecoder.Decode(HttpContext.Request.Headers.Authorization.ToString());
        if (user is null)
        {
            return Unauthorized();
        }

        if (await _favouriteEventRepository.IsFavourite(user.Id, eventId) == false)
        {
            return BadRequest();
        }

        await _favouriteEventRepository.DeleteFavouriteEventAsync(user.Id, eventId);

        return Ok();
    }
}
