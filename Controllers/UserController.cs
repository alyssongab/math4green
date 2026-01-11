using agendamento_recursos.DTOs.User;
using agendamento_recursos.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace agendamento_recursos.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] string email)
        {
            try
            {
                var userExists = await userService.UserExists(email);

                if (!userExists)
                {
                    return NotFound(new { message = "Email não encontrado. Faça o registro." });
                }

                var users = await userService.GetAllUsersAsync();
                var existingUser = users.FirstOrDefault(u => u.Email.ToLowerInvariant().Equals(email.ToLowerInvariant()));

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto dto)
        {
            try
            {
                var newUser = await userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(Login), new { email = newUser.Email }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
