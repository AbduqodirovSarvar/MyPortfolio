namespace MyPortfolio.Application.Models.ViewModels
{
    public class EducationViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string? EducationWebSiteUrl { get; set; }
        public long UserId { get; set; }
    }
}
