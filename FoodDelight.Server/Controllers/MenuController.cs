using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.MenuItem;
using FoodDelight.Server.Models.Restaurant;
using FoodDelight.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        { 
            var result = await _menuItemService.Get(Id);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpGet("menus/{MenuId}")]
        public async Task<IActionResult> GetAll(int MenuId)
        { 
            var result = await _menuItemService.GetAll(MenuId);

            if (result.Count > 0)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpPost("{MenuId}")]
        public async Task<IActionResult> Create(int MenuId, [FromBody] CreateMenuItemDTO create)
        {
            try
            {
                if (create == null)
                {
                    return BadRequest();
                } else
                {
                    var result = await _menuItemService.Create(MenuId, create);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut("{MenuId}/{Id}")]
        public async Task<IActionResult> Update(int MenuId, int Id, [FromBody] CreateMenuItemDTO update)
        {
            try
            {
                if (update == null && Id <= 0)
                {
                    return BadRequest();
                } else
                {
                    var result = await _menuItemService.Update(MenuId, Id, update);
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
            var result = await _menuItemService.Delete(Id);

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
