using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Entities.Identity;
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

            var users = await context.Users
                .Select(x => new { x.Id, x.UserName })
                .ToListAsync();
            var usersDictionary = users.ToDictionary(
                x => x.UserName,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<PlayerRecord>("Players");

            foreach (var record in records)
            {
                if (!usersDictionary.TryGetValue(record.UserName.ToLower(), out var user))
                    throw new Exception($"User '{record.UserName}' not found.");

                var player = _mapper.Map<Player>(record)
                    .SetUser(user.Id);

                context.Players.Add(player);
            }

            await context.SaveChangesAsync();
        }
    }
}
