using System;

namespace BunnyCDN.Api
{
    public class BunnyException : Exception 
    {
        public BunnyException(string message) : base(message) { }
    }

    public class BunnyTokenException : BunnyException
    {
        public string Token { get; private set; }

        public BunnyTokenException(string message, string token) : base(message)
        {
            this.Token = token;
        } 
    }
}