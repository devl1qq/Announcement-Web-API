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
using FluentAssertions;
using Announcement_Web_API.DTOs;

namespace Announcement_Web_API.Tests
{
    public class AnnouncementsEditMethodTests
    {
        private readonly AnnouncementsController _controller;
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementsEditMethodTests()
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
        public async Task EditAnnouncement_ExistingId_ReturnsOkResult()
        {
            var maxId = await _dbContext.Announcements.MaxAsync(a => (int?)a.Id) ?? 0;
            var existingAnnouncement = new Announcement
            {
                Id = maxId + 1, 
                Title = "Existing Announcement",
                Description = "Existing Description",
                Location = "Existing Location",
                DateAdded = DateTime.UtcNow
            };

            _dbContext.Announcements.Add(existingAnnouncement);
            await _dbContext.SaveChangesAsync();

            var updatedAnnouncement = new AnnouncementDTO
            {
                Title = "Updated Title",
                Description = "Updated Description",
                Location = "Updated Location",
                DateAdded = DateTime.UtcNow
            };

            var result = await _controller.EditAnnouncement(existingAnnouncement.Id, updatedAnnouncement);

            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Announcement updated successfully");

            var updatedAnnouncementFromDb = await _dbContext.Announcements.FindAsync(existingAnnouncement.Id);
            updatedAnnouncementFromDb.Should().NotBeNull();
            updatedAnnouncementFromDb.Title.Should().Be(updatedAnnouncement.Title);
            updatedAnnouncementFromDb.Description.Should().Be(updatedAnnouncement.Description);
            updatedAnnouncementFromDb.Location.Should().Be(updatedAnnouncement.Location);
        }




        [Fact]
        public async Task EditAnnouncement_NonExistingId_ReturnsNotFound()
        {
            var nonExistingId = 999;

            var updatedAnnouncement = new AnnouncementDTO
            {
                Title = "Updated Title",
                Description = "Updated Description",
                Location = "Updated Location"
            };

            var result = await _controller.EditAnnouncement(nonExistingId, updatedAnnouncement);

            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be($"Announcement with ID {nonExistingId} not found");
        }
    }
}
