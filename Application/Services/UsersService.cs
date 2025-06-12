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

            var errors = new List<string>();

            // Formato de correo
            var emailRegex = new Regex(@"^[\w\-.]+@([\w\-]+\.)+[\w\-]{2,4}$");
            if (!emailRegex.IsMatch(request.Email))
                errors.Add("Formato de correo inválido.");

            // Formato de contraseña (toma la regex del appsettings)
            var passwordRegex = new Regex(_config["PasswordValidation:Regex"]!);
            if (!passwordRegex.IsMatch(request.Password))
                errors.Add("La contraseña debe tener al menos 8 caracteres, incluir al menos un número, una letra mayúscula y un carácter especial.");

            // Valida si existe el usuario
            var existingUser = await _repository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                errors.Add("El correo ya está registrado.");

            // Si hay errores, los devolvemos todos
            if (errors.Any())
            {
                response.HasError = true;
                response.Errors = errors;
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

            // respuesta de éxito
            response.Id = user.Id;
            response.Created = user.Created;
            response.Modified = user.Modified;
            response.LastLogin = user.LastLogin;
            response.Token = user.Token;
            response.IsActive = user.IsActive;
            // HasError y Errors ya vienen por defecto en false/empty

            return response;
        }



        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}