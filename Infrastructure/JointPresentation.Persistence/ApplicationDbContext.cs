using JointPresentation.Application.Interfaces;
using JointPresentation.Domain;
using JointPresentation.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace JointPresentation.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IApplicationDbContext
    {
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SlideElement> SlideElements { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PresentationConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
