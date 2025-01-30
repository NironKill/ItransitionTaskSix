using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JointPresentation.Domain
{
    public class Presentation
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Name { get; set; }

        public string UserName { get; set; }

        public int CreatedAt { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Membership> Memberships { get; set; }
        public ICollection<Slide> Slides { get; set; }
    }
}
