using Announcement_Web_API.Entities;

namespace Announcement_Web_API.Helpers
{
    public class SearchingHelper
    {
        private readonly AnnouncementDbContext _dbContext;

        public SearchingHelper(AnnouncementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string[] GetKeywords(Announcement announcement)
        {
            var commonWords = _dbContext.IgnoredWords.Select(w => w.Word.ToLower()).ToArray();

            return announcement.Title.Split(' ')
                .Concat(announcement.Description.Split(' '))
                .Where(keyword => !commonWords.Contains(keyword.ToLower()))
                .Distinct()
                .ToArray();
        }

        public bool HasMatchingKeyword(Announcement announcement, string[] keywords)
        {
            return keywords.Any(keyword =>
                announcement.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                announcement.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}