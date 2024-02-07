namespace MyPortfolio.Application.Models.ViewModels
{
    public class CertificateViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CertificateUrl { get; set; }
        public string? Credential { get; set; }
        public DateOnly Issued { get; set; }
        public long UserId { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; } = new HashSet<SkillViewModel>();
    }
}
