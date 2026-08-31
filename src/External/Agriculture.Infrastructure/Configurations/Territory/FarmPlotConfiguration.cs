using Agriculture.Domain.Entities.Territoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agriculture.Infrastructure.Configurations.Territory
{
    internal class FarmPlotConfiguration
        : IEntityTypeConfiguration<FarmPlot>
    {
        public void Configure(EntityTypeBuilder<FarmPlot> builder)
        {
            builder.ToTable("FarmPlots");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.FarmPlots)
                .HasForeignKey(x => x.FarmId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
