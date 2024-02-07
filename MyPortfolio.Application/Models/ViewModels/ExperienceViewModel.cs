namespace MyPortfolio.Application.Models.ViewModels
{
    public class ExperienceViewModel
    {
        public long Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Description { get; set; }
        public string? Position { get; set; }
        public string? WorkType { get; set; }
        public string? City { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; } = new HashSet<SkillViewModel>();
    }
}
