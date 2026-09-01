namespace Agriculture.Domain.Entities.Guest
{
    public partial class Player
    {
        public Player() { }

        public Player SetUser(int userId)
        {
            UserId = userId;
            return this;
        }
    }
}
