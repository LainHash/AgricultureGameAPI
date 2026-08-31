using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Guest;
using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.DataRecords.Guest;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Seeding.Seeders.Guest
{
    internal class PlayerFarmSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.PlayerFarms.AnyAsync())
                return;

            var players = await context.Players
                .Select(x => new { x.Id, x.UserName })
                .ToDictionaryAsync(
                    x => x.UserName,
                    StringComparer.OrdinalIgnoreCase);
            var farms = await context.Farms
                .Select(x => new { x.Id, x.Name })
                .ToDictionaryAsync(
                    x => x.Name,
                    StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<PlayerFarmRecord>("PlayerFarms");
            var entities = new List<PlayerFarm>();

            foreach (var record in records)
            {
                if (!players.TryGetValue(record.UserName, out var player))
                    throw new Exception($"Player '{record.UserName}' not found.");

                if (!farms.TryGetValue(record.FarmName, out var farm))
                    throw new Exception($"Farm '{record.FarmName}' not found.");

                var playerFarm = _mapper.Map<PlayerFarm>(record);
                context.Entry(playerFarm).Property(x => x.PlayerId).CurrentValue = player.Id;
                context.Entry(playerFarm).Property(x => x.FarmId).CurrentValue = farm.Id;
                entities.Add(playerFarm);
            }

            context.PlayerFarms.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
