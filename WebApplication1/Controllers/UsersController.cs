using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _usersService.RegisterAsync(request);

            if (response.HasError)
                return BadRequest(new { message = response.Error });

            return CreatedAtAction(nameof(Register), new
            {
                id = response.Id,
                created = response.Created,
                modified = response.Modified,
                last_login = response.LastLogin,
                token = response.Token,
                isactive = response.IsActive
            });
        }
    }
}
