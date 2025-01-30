using System.ComponentModel.DataAnnotations;

namespace JointPresentation.Domain
{
    public class Membership
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Guid PresentationId { get; set; }

        public int Role { get; set; }

        public User User { get; set; }
        public Presentation Presentation { get; set; }
    }
}
