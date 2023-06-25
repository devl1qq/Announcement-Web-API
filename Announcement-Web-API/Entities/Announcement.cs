using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Announcement_Web_API.Entities
{
    [Table("Announcements")]
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Location { get; set; }
    }
}
