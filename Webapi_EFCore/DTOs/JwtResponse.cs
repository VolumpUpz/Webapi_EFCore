﻿namespace Webapi_EFCore.DTOs
{
    public class JwtResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
