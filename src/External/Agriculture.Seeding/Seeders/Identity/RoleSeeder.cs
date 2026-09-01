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
    internal class RoleSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var records =
                _importer.Read<RoleRecord>("Roles");

            var entities =
                _mapper.Map<List<Role>>(records);

            context.Roles.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
