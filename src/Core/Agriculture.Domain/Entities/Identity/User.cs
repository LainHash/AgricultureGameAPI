using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Identity
{
    public partial class User : SoftDeletableEntity
    {
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        public int RoleId { get; private set; }

        public Role Role { get; private set; } = null!;
        public Player Player { get; private set; } = null!;
    }
}
