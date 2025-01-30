using Microsoft.AspNetCore.Identity;

namespace JointPresentation.Domain
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Presentation> Presentations { get; set; }
        public ICollection<Membership> Memberships { get; set; }
    }
}
