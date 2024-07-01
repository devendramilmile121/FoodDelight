using FoodDelight.Server.Models.Restaurant;
using FoodDelight.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        { 
            var result = await _restaurantService.Get(Id);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var result = await _restaurantService.GetAll();

            if (result.Count > 0)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurant create)
        {
            try
            {
                if (create == null)
                {
                    return BadRequest();
                } else
                {
                    var result = await _restaurantService.Create(create);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] CreateRestaurant update)
        {
            try
            {
                if (update == null && Id <= 0)
                {
                    return BadRequest();
                } else
                {
                    var result = await _restaurantService.Update(Id, update);
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
            var result = await _restaurantService.Delete(Id);

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
