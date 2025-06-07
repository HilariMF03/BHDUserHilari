using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using static Application.Services.JWTGenerator;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IConfiguration _config;
        private readonly IJwtGenerator _jwtGenerator;


        public UsersService(IUsersRepository repository, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _repository = repository;
            _config = config;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterResponse();

            // Valida formato de correo electrónico
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!emailRegex.IsMatch(request.Email))
            {
                response.HasError = true;
                response.Error = "Formato de correo inválido";
                return response;
            }

            // Valida contraseña con expresión regular desde el appsettings.json
            var passwordRegex = new Regex(_config["PasswordValidation:Regex"]);
            if (!passwordRegex.IsMatch(request.Password))
            {
                response.HasError = true;
                response.Error = "Formato de contraseña inválido";
                return response;
            }

            // Verifica si el email ya existe en la bd
            var existingUser = await _repository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                response.HasError = true;
                response.Error = "El correo ya registrado";
                return response;
            }


            var user = new Users
            {
                Name = request.Name,
                Email = request.Email,
                Password = HashPassword(request.Password),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                IsActive = true,
                Phones = request.Phones.Select(p => new Phone
                {
                    Number = p.Number,
                    CityCode = p.CityCode,
                    ContryCode = p.ContryCode
                }).ToList()
            };


            user.Token = _jwtGenerator.GenerateToken(user.Id, user.Email);

            await _repository.AddAsync(user);

            // respuesta
            response.Id = user.Id;
            response.Created = user.Created;
            response.Modified = user.Modified;
            response.LastLogin = user.LastLogin;
            response.Token = user.Token;
            response.IsActive = user.IsActive;

            return response;
        }


        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}