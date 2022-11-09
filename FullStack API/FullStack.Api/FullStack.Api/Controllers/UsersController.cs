using FullStack.Api.Controllers.Models;
using FullStack.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public UsersController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _fullStackDbContext.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            userRequest.Id = Guid.NewGuid();

            await _fullStackDbContext.Users.AddAsync(userRequest);

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(userRequest);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = 
                await _fullStackDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user == null) { return NotFound(); }

            return Ok(user);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, User updateUserRequest)
        {
            var user = await _fullStackDbContext.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            user.FirstName = updateUserRequest.FirstName;
            user.LastName = updateUserRequest.LastName;
            user.Role = updateUserRequest.Role;
            user.IsActive = updateUserRequest.IsActive;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _fullStackDbContext.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            _fullStackDbContext.Remove(user);

            await _fullStackDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
