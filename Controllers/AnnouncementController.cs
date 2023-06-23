using Announcement_Web_API.DTOs;
using Announcement_Web_API.Entities;
using Announcement_Web_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Announcement_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AnnouncementDbContext _dbContext;
        private readonly SearchingHelper _searchingHelper;

        public AnnouncementsController(AnnouncementDbContext dbContext)
        {
            _dbContext = dbContext;
            _searchingHelper = new SearchingHelper(_dbContext);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAnnouncement([FromBody] AnnouncementDTO announcementDTO)
        {
            try
            {
                var announcement = new Announcement
                {
                    Title = announcementDTO.Title,
                    Description = announcementDTO.Description,
                    Location = announcementDTO.Location,
                    DateAdded = DateTime.UtcNow
                };

                _dbContext.Announcements.Add(announcement);
                await _dbContext.SaveChangesAsync();

                var response = new
                {
                    Message = "Announcement added successfully"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add announcement: {ex.Message}");
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var announcement = await _dbContext.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound($"Announcement with ID {id} not found");
                }

                _dbContext.Announcements.Remove(announcement);
                await _dbContext.SaveChangesAsync();

                return Ok("Announcement deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete announcement: {ex.Message}");
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditAnnouncement(int id, [FromBody] AnnouncementDTO announcementDTO)
        {
            try
            {
                var announcement = await _dbContext.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound($"Announcement with ID {id} not found");
                }

                announcement.Title = announcementDTO.Title ?? announcement.Title;
                announcement.Description = announcementDTO.Description ?? announcement.Description;
                announcement.Location = announcementDTO.Location ?? announcement.Location;

                await _dbContext.SaveChangesAsync();

                return Ok("Announcement updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update announcement: {ex.Message}");
            }
        }


        [HttpGet("get_list_of_announcement")]
        public async Task<IActionResult> GetAnnouncementList()
        {
            try
            {
                var announcements = await _dbContext.Announcements
                    .Select(a => new AnnouncementDTO
                    {
                        Title = a.Title,
                        Description = a.Description,
                        Location = a.Location
                    })
                    .ToArrayAsync();

                return Ok(announcements);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve announcement list: {ex.Message}");
            }
        }

        [HttpGet("find/{id}")]
        public IActionResult GetAnnouncementDetails(int id)
        {
            try
            {
                var selectedAnnouncement = _dbContext.Announcements.FirstOrDefault(a => a.Id == id);
                if (selectedAnnouncement == null)
                    return NotFound($"Announcement with ID {id} not found");

                var announcementDetails = new
                {
                    selectedAnnouncement.Title,
                    selectedAnnouncement.Description,
                    selectedAnnouncement.DateAdded
                };

                var selectedAnnouncementKeywords = _searchingHelper.GetKeywords(selectedAnnouncement);

                var allAnnouncements = _dbContext.Announcements.ToList();

                var similarAnnouncements = allAnnouncements
                    .Where(a => a.Id != id && _searchingHelper.HasMatchingKeyword(a, selectedAnnouncementKeywords))
                    .OrderByDescending(a => a.DateAdded)
                    .Take(3)
                    .Select(a => new AnnouncementDTO
                    {
                        Title = a.Title,
                        Description = a.Description,
                        Location = a.Location
                    })
                    .ToArray();

                var result = new
                {
                    AnnouncementDetails = announcementDetails,
                    SimilarAnnouncements = similarAnnouncements
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve announcement details: {ex.Message}");
            }
        }
    }
}
