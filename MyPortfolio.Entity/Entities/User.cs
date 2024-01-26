using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record User : BaseEntity
    {
        public User(
            string firstName,
            string lastName,
            string? middleName,
            string email,
            string password,
            DateOnly dateonly,
            Gender gender,
            string profession,
            string aboutMe,
            string phoneNumber,
            string photoUrl,
            string resumeUrl
            ) :base() 
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            Password = password;
            BirtDay = dateonly;
            Gender = gender;
            Profession = profession;
            AboutMe = aboutMe;
            PhoneNumber = phoneNumber;
            PhotoUrl = photoUrl;
            ResumeUrl = resumeUrl;
        }
        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set;}
        [Required]
        [MailValidation]
        public string Email { get; private set; }
        [Required]
        [PasswordValidation]
        public string Password { get; private set; }
        public string? MiddleName { get; private set; }
        public DateOnly BirtDay { get; private set; }
        public Gender Gender { get; private set; }
        public string Profession { get; private set; }
        public string AboutMe { get; private set; }
        [Required]
        [PhoneNumberValidation]
        public string PhoneNumber { get; private set; }
        [UriValidation]
        public string PhotoUrl { get; private set; }
        [UriValidation]
        public string ResumeUrl { get; private set; }
        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
        public ICollection<UserLanguage> Languages { get; set; } = new HashSet<UserLanguage>();
        public ICollection<Certificate> Certificates { get; set; } = new HashSet<Certificate>();
        public ICollection<Experience> Experiences { get; set; } = new HashSet<Experience>();
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public ICollection<Social> Socials { get; set; } = new HashSet<Social>();
        public ICollection<Education> Educations { get; set; } = new HashSet<Education>();
    }
}
