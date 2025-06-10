namespace Webapi_EFCore.Models
{
    public class RefreshTokenRequest
    {
        public string Username { get; set; }
        public string RefreshToken { get; set; }
    }
}
