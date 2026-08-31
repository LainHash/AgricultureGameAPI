using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.Seeders.Guest;
using Agriculture.Seeding.Seeders.Territory;
using Microsoft.Extensions.DependencyInjection;

namespace Agriculture.Seeding.Seeders
{
    internal class DatabaseSeeder(
        IServiceProvider serviceProvider,
        AgricultureDbContext context)
    {
        private readonly AgricultureDbContext _context = context;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task SeedAllAsync()
        {
            await SeedAsync<PlayerSeeder>(_context);

            await SeedAsync<FarmSeeder>(_context);
            await SeedAsync<FarmPlotSeeder>(_context);

            await SeedAsync<PlayerFarmSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(AgricultureDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
