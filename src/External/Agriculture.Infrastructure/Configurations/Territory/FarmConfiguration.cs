using Agriculture.Domain.Entities.Territoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agriculture.Infrastructure.Configurations.Territory
{
    internal class FarmConfiguration
        : IEntityTypeConfiguration<Farm>
    {
        public void Configure(EntityTypeBuilder<Farm> builder)
        {
            builder.ToTable("Farms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.HasOne(x => x.Player)
                .WithOne(x => x.Farm)
                .HasForeignKey<Farm>(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
