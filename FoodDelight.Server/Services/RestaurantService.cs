using FoodDelight.Server.Data;
using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Restaurant;
using FoodDelight.Server.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace FoodDelight.Server.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RestaurantDTO> Create(CreateRestaurant restaurant)
        {
            Restaurant newRestaurant = _mapper.Map<Restaurant>(restaurant);
            _context.Add(newRestaurant);
            await _context.SaveChangesAsync();

            return await Get(newRestaurant.Id);
        }

        public async Task<bool> Delete(int Id)
        {
            Restaurant restaurant = await _context.Restaurants.Where(x => x.Id == Id)
                .Include(inc => inc.Menus)
                    .ThenInclude(inc => inc.MenuItems)
                .FirstOrDefaultAsync();

            if (restaurant != null)
            {
                _context.Remove(restaurant);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        public async Task<RestaurantDTO> Get(int Id)
        {
            Restaurant restaurant = await _context.Restaurants.Where(x => x.Id == Id)
                .Include(inc => inc.Menus)
                    .ThenInclude(inc => inc.MenuItems)
                .FirstOrDefaultAsync();
            RestaurantDTO restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
        }

        public async Task<List<RestaurantDTO>> GetAll()
        {
            List<Restaurant> restaurant = await _context.Restaurants.ToListAsync();

            List<RestaurantDTO> restaurantDTO = _mapper.Map<List<RestaurantDTO>>(restaurant);
            return restaurantDTO;
        }

        public async Task<RestaurantDTO> Update(int Id, CreateRestaurant restaurant)
        {
            var oldRes = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == Id);

            if (oldRes == null)
            {
                throw new KeyNotFoundException($"Restaurant with ID {Id} not found.");
            }

            _mapper.Map(restaurant, oldRes);

            await _context.SaveChangesAsync();

            return await Get(Id);
            }
    }
}
