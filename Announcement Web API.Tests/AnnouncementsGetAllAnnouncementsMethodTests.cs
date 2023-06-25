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

namespace Announcement_Web_API.Tests
{
    public class AnnouncementsGetAllAnnouncementsMethodTests
    {
        private readonly AnnouncementsController _controller;
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementsGetAllAnnouncementsMethodTests()
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
        public async Task GetAnnouncementList_ReturnsOkResultWithAnnouncements()
        {
            var announcements = new List<Announcement>
            {
                new Announcement
                {
                    Id = 1,
                    Title = "Announcement 1",
                    Description = "Description 1",
                    Location = "Location 1"
                },
                new Announcement
                {
                    Id = 2,
                    Title = "Announcement 2",
                    Description = "Description 2",
                    Location = "Location 2"
                }
            };

            _dbContext.Announcements.AddRange(announcements);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.GetAnnouncementList();

            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<Announcement[]>();

            var returnedAnnouncements = okResult.Value as Announcement[];
            returnedAnnouncements.Should().HaveCount(announcements.Count);

            for (int i = 0; i < announcements.Count; i++)
            {
                returnedAnnouncements[i].Id.Should().Be(announcements[i].Id);
                returnedAnnouncements[i].Title.Should().Be(announcements[i].Title);
                returnedAnnouncements[i].Description.Should().Be(announcements[i].Description);
                returnedAnnouncements[i].Location.Should().Be(announcements[i].Location);
            }
        }

        [Fact]
        public async Task GetAnnouncementList_ReturnsEmptyList()
        {
            _dbContext.Announcements.RemoveRange(_dbContext.Announcements);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.GetAnnouncementList();

            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<Announcement[]>();

            var returnedAnnouncements = okResult.Value as Announcement[];
            returnedAnnouncements.Should().BeEmpty();
        }
    }
}
