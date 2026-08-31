using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Agriculture.Infrastructure.Context
{
    public class AgricultureDbContext(DbContextOptions<AgricultureDbContext> options) 
        : DbContext(options)
    {

        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<PlayerFarm> PlayerFarms { get; set; } = null!;

        public DbSet<Farm> Farms { get; set; } = null!;
        public DbSet<FarmPlot> FarmPlots { get; set; } = null!;

        // ── Model building ──────────────────────────────────────────────────
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto-register all IEntityTypeConfiguration<T> classes in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // ── Auto-set audit fields on SaveChanges ────────────────────────────
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.MarkCreated(now);
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.MarkUpdated(now);
                }
            }
        }
    }
}
