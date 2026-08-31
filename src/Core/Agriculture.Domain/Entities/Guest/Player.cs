using Agriculture.Domain.Entities.Territoy;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Guest
{
    public class Player : SoftDeletableEntity
    {
        public string Username { get; set; } = null!;
        public int Level { get; set; }
        public long Experience { get; set; }

        public Farm Farm { get; set; } = null!;
    }
}
