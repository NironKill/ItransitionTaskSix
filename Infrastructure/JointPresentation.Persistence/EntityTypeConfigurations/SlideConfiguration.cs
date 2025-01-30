using JointPresentation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JointPresentation.Persistence.EntityTypeConfigurations
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder) => builder.HasMany(s => s.Elements).WithOne(e => e.Slide).HasForeignKey(s => s.SlideId);     
    }
}
