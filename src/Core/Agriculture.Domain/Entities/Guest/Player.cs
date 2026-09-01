using Agriculture.Domain.Entities.Identity;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Guest
{
    public partial class Player : SoftDeletableEntity
    {
        public int Level { get; private set; }
        public long Experience { get; private set; }

        public int UserId { get; private set; }

        public User User { get; private set; } = null!;
        public ICollection<PlayerFarm> PlayerFarms { get; private set; } = [];
    }
}
