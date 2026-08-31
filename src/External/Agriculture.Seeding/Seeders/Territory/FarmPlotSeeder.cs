using Agriculture.Application.Services.Business;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Infrastructure.Context;
using Agriculture.Seeding.DataRecords.Territory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Seeding.Seeders.Territory
{
    internal class FarmPlotSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(AgricultureDbContext context)
        {
            if (await context.FarmPlots.AnyAsync())
                return;

            var farms = await context.Farms
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var farmsDictionary = farms.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<FarmPlotRecord>("FarmPlots");

            foreach (var record in records)
            {
                if (!farmsDictionary.TryGetValue(record.FarmName.ToLower(), out var farm))
                    throw new Exception($"Farm '{record.FarmName}' not found.");

                var farmPlot = _mapper.Map<FarmPlot>(record)
                    .SetFarm(farm.Id);

                context.FarmPlots.Add(farmPlot);
            }

            await context.SaveChangesAsync();
        }
    }
}
