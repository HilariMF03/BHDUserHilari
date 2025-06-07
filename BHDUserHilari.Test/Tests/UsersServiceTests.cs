using Castle.Core.Configuration;
using Moq;
using static Application.Services.JWTGenerator;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Services;
using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

namespace BHDUserHilari.Test.Tests
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IJwtGenerator> _jwtGeneratorMock;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IUsersService _usersService;

        public UsersServiceTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _jwtGeneratorMock = new Mock<IJwtGenerator>();

            var inMemorySettings = new Dictionary<string, string>
        {
            { "PasswordValidation:Regex", "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$" }
        };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _usersService = new UsersService(
                _usersRepositoryMock.Object,
                _config,
                _jwtGeneratorMock.Object
                
            );
        }

        [Fact]
        public async Task RegisterAsync_Should_Return_Error_When_Email_Invalid()
        {
            var request = new RegisterRequest
            {
                Name = "Test",
                Email = "invalidemail",
                Password = "Abcdef12",   // cumple regex salvo el email
                Phones = new List<PhoneRequest>()
            };

            var result = await _usersService.RegisterAsync(request);

            Assert.True(result.HasError);
            Assert.Single(result.Errors);
            Assert.Equal("Formato de correo inválido.", result.Errors[0]);
        }

        [Fact]
        public async Task RegisterAsync_Should_Return_Error_When_Password_Invalid()
        {
            var request = new RegisterRequest
            {
                Name = "Test",
                Email = "test@email.com",
                Password = "abc",  // no cumple regex
                Phones = new List<PhoneRequest>()
            };

            var result = await _usersService.RegisterAsync(request);

            Assert.True(result.HasError);
            Assert.Single(result.Errors);
            Assert.Equal("Formato de contraseña inválido. Debe tener 8 caracteres, al menos una mayúscula y un número.", result.Errors[0]);
        }

        [Fact]
        public async Task RegisterAsync_Should_Return_Error_When_Email_Exists()
        {
            _usersRepositoryMock
                .Setup(repo => repo.GetByEmailAsync("test@email.com"))
                .ReturnsAsync(new Users { Email = "test@email.com" });

            var request = new RegisterRequest
            {
                Name = "Test",
                Email = "test@email.com",
                Password = "Abcdef12",
                Phones = new List<PhoneRequest>()
            };

            var result = await _usersService.RegisterAsync(request);

            Assert.True(result.HasError);
            Assert.Single(result.Errors);
            Assert.Equal("El correo ya está registrado.", result.Errors[0]);
        }

        [Fact]
        public async Task RegisterAsync_Should_Succeed_When_Valid()
        {
            _usersRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Users)null);
            _jwtGeneratorMock
                .Setup(jwt => jwt.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns("mocked-token");

            var request = new RegisterRequest
            {
                Name = "Test",
                Email = "test@email.com",
                Password = "Abcdef12",
                Phones = new List<PhoneRequest>
            {
                new PhoneRequest { Number = "123", CityCode = "1", ContryCode = "57" }
            }
            };

            var result = await _usersService.RegisterAsync(request);

            Assert.False(result.HasError);
            Assert.Empty(result.Errors);

            Assert.Equal("mocked-token", result.Token);
            Assert.True(result.IsActive);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
