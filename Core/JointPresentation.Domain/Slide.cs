using System.ComponentModel.DataAnnotations;

namespace JointPresentation.Domain
{
    public class Slide
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PresentationId { get; set; }

        public int Order { get; set; }

        public Presentation Presentation { get; set; }
        public ICollection<SlideElement> Elements { get; set; }
    }
}
