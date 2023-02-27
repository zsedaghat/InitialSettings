namespace SpaceManagment.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public long? UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public DateTime RefreshTokenCreated { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public User User { get; set; }
    }
}
