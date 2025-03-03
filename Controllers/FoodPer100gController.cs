using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using macro_tracker_core_service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace macro_tracker_core_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly MacroTrackerContext _context;

        public FoodsController(MacroTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodPer100g>>> GetAllFoods()
        {
            return await _context.FoodsPer100g.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodPer100g>> GetFoodById(int id)
        {
            var food = await _context.FoodsPer100g.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }
    }
}