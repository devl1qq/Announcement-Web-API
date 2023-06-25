using Xunit;
using Announcement_Web_API.Controllers;
using Announcement_Web_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Announcement_Web_API.Tests
{
    public class AnnouncementsDeleteMethodTests
    {
        private readonly AnnouncementsController _controller;
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementsDeleteMethodTests()
        {
            var options = new DbContextOptionsBuilder<AnnouncementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TestAnnouncementDbConnection");

            var serviceProvider = new ServiceCollection()
                .AddDbContext<AnnouncementDbContext>(options => options.UseNpgsql(connectionString))
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<AnnouncementDbContext>();
            _controller = new AnnouncementsController(_dbContext);
        }


        [Fact]
        public async Task DeleteAnnouncement_ValidId_ReturnsOkResult()
        {
            var announcement = new Announcement
            {
                Id = GenerateUniqueTestId(),
                Title = "Test Announcement",
                Description = "Test Description",
                Location = "Test Location",
                DateAdded = DateTime.UtcNow
            };

            _dbContext.Announcements.Add(announcement);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeleteAnnouncement(announcement.Id);

            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal("Announcement deleted successfully", okResult.Value);
        }

        [Fact]
        public async Task DeleteAnnouncement_InvalidId_ReturnsNotFound()
        {
            var invalidId = GenerateUniqueTestId();
            
            var result = await _controller.DeleteAnnouncement(invalidId);

            Assert.IsType<NotFoundObjectResult>(result);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal($"Announcement with ID {invalidId} not found", notFoundResult.Value);
        }

        private int GenerateUniqueTestId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}
