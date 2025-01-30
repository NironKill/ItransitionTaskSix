using JointPresentation.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JointPresentation.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Presentation> Presentations { get; set; }
        DbSet<Slide> Slides { get; set; }
        DbSet<SlideElement> SlideElements { get; set; }
        DbSet<Membership> Memberships { get; set; }

        DbSet<User> Users { get; set; }
        DbSet<IdentityUserToken<Guid>> UserTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
