using AuthAPI.Models;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net.Http;
namespace AuthAPI.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock <IConfiguration> _mockConfig;
        private readonly Mock <IHttpClientFactory> _mockHttpFactory;
        private readonly Mock <IUnitOfWork> _mockUnitOfWork;
        public AuthServiceTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockHttpFactory = new Mock<IHttpClientFactory>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task AssignRoleAsync_InvalidRole_ReturnError()
        {
            var authService = new AuthService(_mockUnitOfWork.Object, _mockConfig.Object, _mockHttpFactory.Object);
            string email = "test@gmail.com";
            string invalidRole = "SuperAdmin";
            var result = await authService.AssignRoleAsync(email, invalidRole);
            Assert.Equal("Không hợp lệ", result);
        }
    }
}