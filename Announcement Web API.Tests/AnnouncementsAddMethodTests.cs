using Xunit;
using Announcement_Web_API.Controllers;
using Announcement_Web_API.DTOs;
using Announcement_Web_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Announcement_Web_API.Tests
{
    public class AnnouncementsAddMethodTests
    {
        private readonly AnnouncementsController _controller;
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementsAddMethodTests()
        {
            var options = new DbContextOptionsBuilder<AnnouncementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
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
        public async Task AddAnnouncement_ValidAnnouncement_ReturnsOkResult()
        {
            var announcementDTO = new AnnouncementDTO
            {
                Title = "Test Announcement",
                Description = "Test Description",
                Location = "Test Location",
                DateAdded = DateTime.UtcNow
            };

            try
            {
                var result = await _controller.AddAnnouncement(announcementDTO);

                if (result is BadRequestObjectResult badRequestResult)
                {
                    var errorMessage = badRequestResult.Value.ToString();
                    Assert.True(false, $"Failed to add announcement: {errorMessage}");
                }

                Assert.IsType<OkObjectResult>(result);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Failed to add announcement: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Assert.True(false, $"Inner exception: {ex.InnerException.Message}");
                }
            }

        }


    }
}
