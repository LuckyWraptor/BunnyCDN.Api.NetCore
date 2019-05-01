using System;

namespace BunnyCDN.Api
{
    public class BunnyException : Exception 
    {
        /// <summary>
        /// A BunnyException wrapper around the Exception, used to catch all BunnyCDN exceptions.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN exception</returns>
        public BunnyException(string message = null) : base(message) { }
    }
    public class BunnyTokenException : BunnyException
    {
        /// <summary>
        /// Token (if any) causing the error.
        /// </summary>
        /// <value>Token-string or null if none</value>
        public string Token { get; private set; }

        /// <summary>
        /// Token exception used for Token faults.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN token exception</returns>
        public BunnyTokenException(string message) : base(message) {}
        
        /// <summary>
        /// Token exception used for Token faults.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <param name="token">Token causing the exception</param>
        /// <returns>A BunnyCDN token exception</returns>
        public BunnyTokenException(string message, string token) : base(message)
        {
            this.Token = token;
        } 
    }
    public class BunnyZoneException : BunnyException { }
    public class BunnyNotFoundException : BunnyException { }
    public class BunnyUnauthorizedException : BunnyException { }
    public class BunnyBadRequestException : BunnyException
    {
        /// <summary>
        /// Bad Request exception used for input faults.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN BadRequest exception</returns>
        public BunnyBadRequestException(string message) : base(message) {}
    }
    public class BunnyInvalidResponseException : BunnyException
    {
        /// <summary>
        /// Invalid response exception used for invalid JSON returns
        /// </summary>
        /// <returns>A BunnyCDN InvalidResponse exception</returns>
        public BunnyInvalidResponseException() {}
        /// <summary>
        /// Invalid response exception used for invalid JSON returns
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN InvalidResponse exception</returns>
        public BunnyInvalidResponseException(string message) : base(message) {}
    }
}