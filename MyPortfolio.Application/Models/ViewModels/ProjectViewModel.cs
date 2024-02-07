namespace MyPortfolio.Application.Models.ViewModels
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public long UserId { get; set; }
        public string? UrlToCode { get; set; }
        public string? UrlToSite { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; } = new HashSet<SkillViewModel>();
    }
}
