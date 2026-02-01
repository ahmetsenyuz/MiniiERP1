namespace MiniiERP1.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationHours { get; set; }
    }
}