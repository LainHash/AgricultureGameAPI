using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Guest;
using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.DataRecords.Guest;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Seeding.Seeders.Guest
{
    internal class PlayerSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.Players.AnyAsync())
                return;

            var records =
                _importer.Read<PlayerRecord>("Players");

            var entities =
                _mapper.Map<List<Player>>(records);

            context.Players.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
