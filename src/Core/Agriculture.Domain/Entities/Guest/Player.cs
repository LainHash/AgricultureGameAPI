using Agriculture.Domain.Entities.Territoy;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Guest
{
    public class Player : SoftDeletableEntity
    {
        public string UserName { get; private set; } = null!;
        public int Level { get; private set; }
        public long Experience { get; private set; }

        public ICollection<PlayerFarm> PlayerFarms { get; private set; } = [];
    }
}
