namespace Administration.Jwt
{
    /// <summary>
    /// The class is designed to transfer data 
    /// about the JWT token to the service
    /// </summary>
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
