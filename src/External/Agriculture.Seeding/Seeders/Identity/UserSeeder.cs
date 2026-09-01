using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Identity;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.DataRecords.Identity;
using Agriculture.Seeding.DataRecords.Territory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Seeding.Seeders.Identity
{
    internal class UserSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var roles = await context.Roles
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var rolesDictionary = roles.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<UserRecord>("Users");

            foreach (var record in records)
            {
                if (!rolesDictionary.TryGetValue(record.RoleName.ToLower(), out var role))
                    throw new Exception($"Role '{record.RoleName}' not found.");

                var user = _mapper.Map<User>(record)
                    .SetRole(role.Id);

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
