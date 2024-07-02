using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.Restaurant;
using FoodDelight.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        { 
            var result = await _menuService.Get(Id);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpGet("restaurant/{RestaurantId}")]
        public async Task<IActionResult> GetAll(int RestaurantId)
        { 
            var result = await _menuService.GetAll(RestaurantId);

            if (result.Count > 0)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpPost("{RestaurantId}")]
        public async Task<IActionResult> Create(int RestaurantId, [FromBody] CreateMenuDTO create)
        {
            try
            {
                if (create == null)
                {
                    return BadRequest();
                } else
                {
                    var result = await _menuService.Create(RestaurantId, create);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut("{RestaurantId}/{Id}")]
        public async Task<IActionResult> Update(int RestaurantId, int Id, [FromBody] CreateMenuDTO update)
        {
            try
            {
                if (update == null && Id <= 0)
                {
                    return BadRequest();
                } else
                {
                    var result = await _menuService.Update(RestaurantId, Id, update);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _menuService.Delete(Id);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
