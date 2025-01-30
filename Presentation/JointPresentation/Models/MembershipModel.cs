namespace JointPresentation.Models
{
    public class MembershipModel
    {
        public int Id { get; set; }
        public Guid PresentationId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
    }
}
