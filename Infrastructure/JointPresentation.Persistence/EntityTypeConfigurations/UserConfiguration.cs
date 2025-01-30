using JointPresentation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JointPresentation.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.HasMany(u => u.Presentations).WithMany(p => p.Users).UsingEntity<Membership>(
                x => x.HasOne<Presentation>(m => m.Presentation).WithMany(p => p.Memberships).HasForeignKey(m => m.PresentationId),
                x => x.HasOne<User>(m => m.User).WithMany(u => u.Memberships).HasForeignKey(m => m.UserId));

            builder.HasIndex(u => u.UserName).IsUnique();
        }      
    }
}
