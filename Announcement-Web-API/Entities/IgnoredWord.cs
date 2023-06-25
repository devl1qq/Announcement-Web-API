using System.ComponentModel.DataAnnotations.Schema;

namespace Announcement_Web_API.Entities
{
    [Table("IgnoredWords")]
    public class IgnoredWord
    {
        public string Id { get; set; }
        public string Word { get; set; }
    }
}
