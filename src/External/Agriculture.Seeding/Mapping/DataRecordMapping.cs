using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Entities.Identity;
using Agriculture.Domain.Entities.Territoy;
using Agriculture.Seeding.DataRecords.Guest;
using Agriculture.Seeding.DataRecords.Identity;
using Agriculture.Seeding.DataRecords.Territory;
using AutoMapper;

namespace Agriculture.Seeding.Mapping
{
    internal class DataRecordMapping
        : Profile
    {
        public DataRecordMapping()
        {
            CreateMap<RoleRecord, Role>();
            CreateMap<UserRecord, User>()
                .ForMember(x => x.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.PasswordHash)));

            CreateMap<PlayerRecord, Player>();
            CreateMap<PlayerFarmRecord, PlayerFarm>()
                .ForMember(x => x.UnlockedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<FarmRecord, Farm>();
            CreateMap<FarmPlotRecord, FarmPlot>();
        }
    }
}
