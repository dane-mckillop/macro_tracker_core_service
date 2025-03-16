using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using macro_tracker_core_service.Models;
using System.Threading.Tasks;

namespace macro_tracker_core_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MacroTrackerContext _context;

        public UsersController(MacroTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Users/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<object>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Return user data without HashPassword
            return new
            {
                user.UserId,
                user.Username,
                user.Age,
                user.Height,
                user.Weight
            };
        }
    }
}