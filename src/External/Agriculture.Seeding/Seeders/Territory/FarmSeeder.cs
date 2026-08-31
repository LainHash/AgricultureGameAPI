using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.DataRecords.Territory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Seeding.Seeders.Territory
{
    internal class FarmSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.Farms.AnyAsync())
                return;

            var records =
                _importer.Read<FarmRecord>("Farms");

            var entities =
                _mapper.Map<List<Farm>>(records);

            context.Farms.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
