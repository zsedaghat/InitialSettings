namespace SpaceManagment.Common
{
    public class Settings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public Seq Seq { get; set; }
    }

    public class ConnectionStrings
    {
        public string SqlServer { get; set; }
    }
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
    }

    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
    public class Seq
    {
        public string ServerUrl { get; set; }
        public string ApiKey { get; set; }
        public string MinimumLevel { get; set; }


    }
    
}
