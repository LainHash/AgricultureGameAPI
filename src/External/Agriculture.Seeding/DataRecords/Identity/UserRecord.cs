namespace Agriculture.Seeding.DataRecords.Identity
{
    internal class UserRecord
    {
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
