using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Abstraction
{
    public abstract record BaseEntity
    {
        protected BaseEntity()
        { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }
        public DateTime CreatedTime { get; private set; } = DateTime.UtcNow;

        public abstract object Change(object obj);
    }
}
