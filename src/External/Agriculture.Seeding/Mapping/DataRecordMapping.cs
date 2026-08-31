using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Seeding.DataRecords.Guest;
using Agriculture.Seeding.DataRecords.Territory;
using AutoMapper;

namespace Agriculture.Seeding.Mapping
{
    internal class DataRecordMapping
        : Profile
    {
        public DataRecordMapping()
        {
            CreateMap<PlayerRecord, Player>();
            CreateMap<PlayerFarmRecord, PlayerFarm>()
                .ForMember(x => x.UnlockedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<FarmRecord, Farm>();
            CreateMap<FarmPlotRecord, FarmPlot>();
        }
    }
}
