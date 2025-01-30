using System.ComponentModel.DataAnnotations;

namespace JointPresentation.Domain
{
    public class SlideElement
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SlideId { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Color { get; set; }

        public Slide Slide { get; set; }
    }
}
