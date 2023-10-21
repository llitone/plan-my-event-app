using events_service.Dtos;
using events_service_data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace events_service.Controllers;

[ApiController]
[Route("events-service/api/v1/categories/")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet, Route("all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = (await _categoryRepository.GetCategoriesAsync()).Select(c => new CategoryDto()
        {
            Id = c.Id,
            Name = c.Name
        });

        return new JsonResult(result);
    }
}
