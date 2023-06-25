using Xunit;
using Announcement_Web_API.Controllers;
using Announcement_Web_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Announcement_Web_API.DTOs;

namespace Announcement_Web_API.Tests
{
    public class AnnouncementsGetAnnouncementDetailsMethodTests
    {
        private readonly AnnouncementsController _controller;
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementsGetAnnouncementDetailsMethodTests()
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
        public async Task GetAnnouncementDetails_ReturnsNotFoundResult()
        {
            // Arrange 
            int nonExistingAnnouncementId = 999;

            // Act 
            var result = await _controller.GetAnnouncementDetails(nonExistingAnnouncementId);

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Announcement with ID {nonExistingAnnouncementId} not found", notFoundResult.Value);
        }

    }
}