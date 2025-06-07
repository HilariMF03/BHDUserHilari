using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IConfiguration _config;

        public UsersService(IUsersRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterResponse();

            // Validar email duplicado
            var existingUser = await _repository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                response.HasError = true;
                response.Error = "El correo ya registrado";
                return response;
            }

            // Validar formato de email
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!emailRegex.IsMatch(request.Email))
            {
                response.HasError = true;
                response.Error = "Formato de correo inválido";
                return response;
            }

            // Validar contraseña con regex configurable
            var passwordRegex = new Regex(_config["PasswordValidation:Regex"]);
            if (!passwordRegex.IsMatch(request.Password))
            {
                response.HasError = true;
                response.Error = "Formato de contraseña inválido";
                return response;
            }

            // Crear usuario
            var user = new Users
            {
                Name = request.Name,
                Email = request.Email,
                Password = HashPassword(request.Password),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                IsActive = true,
                Token = GenerateToken(),
                Phones = request.Phones.Select(p => new Phone
                {
                    Number = p.Number,
                    CityCode = p.CityCode,
                    ContryCode = p.ContryCode
                }).ToList()
            };

            await _repository.AddAsync(user);

            // Construir respuesta
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
            // Método simple de hash (reemplazar con algo más seguro en prod)
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        private string GenerateToken()
        {
            // Puedes reemplazar por generación de JWT
            return Guid.NewGuid().ToString();
        }
    }
}