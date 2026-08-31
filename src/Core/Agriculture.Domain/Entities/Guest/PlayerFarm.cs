using Agriculture.Domain.Entities.Territoy;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Guest
{
    public class PlayerFarm : SoftDeletableEntity
    {
        public int PlayerId { get; private set; }
        public int FarmId { get; private set; }

        public DateTime UnlockedAt { get; private set; }

        public bool IsActive { get; private set; }

        public Player Player { get; private set; } = null!;
        public Farm Farm { get; private set; } = null!;

    }
}
