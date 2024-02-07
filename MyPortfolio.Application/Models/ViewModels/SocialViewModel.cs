namespace MyPortfolio.Application.Models.ViewModels
{
    public class SocialViewModel
    {
        public long Id { get; set; }
        public string? SocialNetwork { get; set; }
        public string? Url { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
