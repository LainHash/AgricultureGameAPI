using Agriculture.Infrastructure.Context;

namespace Agriculture.Seeding.Seeders
{
    internal interface IDataSeeder
    {
        Task SeedAsync(AgricultureDbContext context);
    }
}
