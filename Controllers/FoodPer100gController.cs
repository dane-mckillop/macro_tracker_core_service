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

        private bool FoodExists(int id)
        {
            return _context.FoodsPer100g.Any(e => e.FoodId == id);
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodPer100g>>> GetAllFoods()
        {
            return await _context.FoodsPer100g.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FoodPer100g>> GetFoodById(int id)
        {
            var food = await _context.FoodsPer100g.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return food;
        }

        // GET: api/Foods/name/Pork
        [HttpGet("name/{name}")]
        public async Task<ActionResult<FoodPer100g>> GetFoodByName(string name)
        {
            var food = await _context.FoodsPer100g.FirstOrDefaultAsync(f => f.Name == name);
            if (food == null)
            {
                return NotFound();
            }
            return food;
        }

        // GET: api/Foods/substring/Pork
        [HttpGet("substring/{name}")]
        public async Task<ActionResult<FoodPer100g>> GetFoodBySubstring(string name)
        {
            var foods = await _context.FoodsPer100g
                .Where(f => f.Name.Contains(name))
                .ToListAsync();
            if (foods == null || !foods.Any())
            {
                return NotFound();
            }
            return Ok(foods);
        }

        // POST: api/Foods
        [HttpPost]
        public async Task<ActionResult<FoodPer100g>> CreateFood(FoodPer100g food)
        {
            if (food == null)
            {
                return BadRequest("Food item cannot be null");
            }

            //Insert food item, exceptions handled by middleware
            _context.FoodsPer100g.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodById), new { id = food.FoodId }, food);
        }

        // PUT: api/Foods/5
        [HttpPut]
        public async Task<IActionResult> UpdateFood(FoodPer100g food)
        {
            if (food == null)
            {
                return BadRequest("Food item cannot be null");
            }

            var existingFood = await _context.FoodsPer100g.FindAsync(food.FoodId);
            if (existingFood == null)
            {
                return NotFound($"Food item with ID {food.FoodId} was not found");
            }

            _context.Entry(existingFood).CurrentValues.SetValues(food);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(food.FoodId))
                {
                    return NotFound($"Food item with ID {food.FoodId} no longer exists");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}