namespace Agriculture.Domain.Entities.Identity
{
    public partial class User
    {
        public User() { }

        public User SetRole(int roleId)
        {
            RoleId = roleId;
            return this;
        }
    }
}
