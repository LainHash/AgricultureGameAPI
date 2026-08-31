using Agriculture.Domain.Entities.Guest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agriculture.Infrastructure.Configurations.Guest
{
    internal class PlayerFarmConfiguration
        : IEntityTypeConfiguration<PlayerFarm>
    {
        public void Configure(EntityTypeBuilder<PlayerFarm> builder)
        {
            builder.ToTable("PlayerFarms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.HasIndex(x => new { x.PlayerId, x.FarmId })
                .IsUnique();

            builder.HasOne(x => x.Player)
                .WithMany(x => x.PlayerFarms)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.PlayerFarms)
                .HasForeignKey(x => x.FarmId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
