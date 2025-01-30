namespace JointPresentation.Application.DTOs
{
    public class MembershipDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PresentationId { get; set; }
        public int Role { get; set; }
    }
}
