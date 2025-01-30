using JointPresentation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JointPresentation.Persistence.EntityTypeConfigurations
{
    public class PresentationConfiguration : IEntityTypeConfiguration<Presentation>
    {
        public void Configure(EntityTypeBuilder<Presentation> builder) => builder.HasMany(p => p.Slides).WithOne(s => s.Presentation).HasForeignKey(s => s.PresentationId);    
    }
}
