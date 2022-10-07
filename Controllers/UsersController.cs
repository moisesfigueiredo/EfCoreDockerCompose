using EfCoreDockerCompose.Context;
using EfCoreDockerCompose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDockerCompose.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _usersDbContext;
        public UsersController(ApplicationDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(await _usersDbContext.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usersDbContext.Users.AddAsync(user);

            await _usersDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User userFromJson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _usersDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = userFromJson.Name;
            user.Age = userFromJson.Age;
            user.Email = userFromJson.Email;

            await _usersDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = await _usersDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _usersDbContext.Remove(user);

            await _usersDbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
