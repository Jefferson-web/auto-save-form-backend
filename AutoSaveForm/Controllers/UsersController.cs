using AutoSaveForm.Database;
using AutoSaveForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSaveForm.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {

        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user) {
            user.id = Guid.NewGuid().ToString("D");
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public async Task<IEnumerable<User>> GetUsers() {
            return await _context.Users.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] User user) {
            
            if (id != user.id) {
                return BadRequest("Primary key don't match");
            }

            User oldUser = await _context.Users.FindAsync(id);

            if (oldUser == null) {
                return NotFound("User not found");
            }

            oldUser.name = user.name;
            oldUser.lastname = user.lastname;

            _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();

            return Ok(oldUser);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.User))]
        public async Task<ActionResult<User>> GetUser(string id) {
            User user =  await _context.Users.FindAsync(id);
            if (user == null) {
                return BadRequest("User not found");
            }
            return Ok(user);
        }

        [HttpPut("markAsRegistered/{id}")]
        public async Task<ActionResult<User>> markAsRegistered(string id) {
            User user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            user.registered = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id) {

            User user = await _context.Users.FindAsync(id);

            if (user == null) {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

    }
}
