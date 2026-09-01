using Agriculture.Domain.Entities.Territoy;
using Agriculture.Domain.Repositories.Territory;
using Agriculture.Infrastructure.Context;

namespace Agriculture.Infrastructure.Repositories.Territory
{
    internal class FarmRepository(AgricultureDbContext context) 
        : Repository<Farm>(context), IFarmRepository
    {
        private readonly AgricultureDbContext _context = context;
    }
}
