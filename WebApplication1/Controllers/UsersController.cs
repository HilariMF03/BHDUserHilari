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
            {
                // Devuelve un array de errores
                return BadRequest(new { errors = response.Errors });
            }

            // Devuelve 201 Created con el cuerpo del nuevo usuario, sin Location
            return Created(
                uri: string.Empty,
                value: new
                {
                    id = response.Id,
                    created = response.Created,
                    modified = response.Modified,
                    last_login = response.LastLogin,
                    token = response.Token,
                    is_active = response.IsActive
                }
            );
        }

    }
}
