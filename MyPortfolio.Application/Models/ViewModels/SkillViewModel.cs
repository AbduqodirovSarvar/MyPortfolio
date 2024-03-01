namespace MyPortfolio.Application.Models.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
