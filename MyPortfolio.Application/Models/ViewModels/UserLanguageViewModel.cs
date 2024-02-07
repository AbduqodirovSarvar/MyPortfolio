namespace MyPortfolio.Application.Models.ViewModels
{
    public class UserLanguageViewModel
    {
        public long Id { get; private set; }
        public LanguageViewModel? Language { get; set; }
        public string? LanguageLevel { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
