using System;

namespace BunnyCDN.Api
{
    /// <summary>
    /// Generic BunnyException to catch all (handled) wrapper errors.
    /// </summary>
    public class BunnyException : Exception 
    {
        /// <summary>
        /// A BunnyException wrapper around the Exception, used to catch all BunnyCDN exceptions.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN exception</returns>
        public BunnyException(string message = null) : base(message) { }
    }
    /// <summary>
    /// Token BunnyException to catch token errors.
    /// </summary>
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
    /// <summary>
    /// Zone BunnyException to catch Zone errors.
    /// </summary>
    public class BunnyZoneException : BunnyException { }
    /// <summary>
    ///  NotFound BunnyException to catch api 404 responses.
    /// </summary>
    public class BunnyNotFoundException : BunnyException { }
    /// <summary>
    /// Unauthorized BunnyException to catch unauthorized requests.
    /// </summary>
    public class BunnyUnauthorizedException : BunnyException { }
    /// <summary>
    /// BadRequest BunnyException to catch bad requests
    /// </summary>
    public class BunnyBadRequestException : BunnyException
    {
        /// <summary>
        /// Error massage response content.
        /// </summary>
        public ErrorMessage Error { get; private set; }
        /// <summary>
        /// Bad Request exception used for input faults.
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN BadRequest exception</returns>
        public BunnyBadRequestException(string message) : base(message) {}
        /// <summary>
        /// Error massage response content.
        /// </summary>
        /// <param name="error">Error message context</param>
        public BunnyBadRequestException(ErrorMessage error) : base(error.Message) {
            this.Error = error;
        }
    }
    /// <summary>
    /// InvalidResponse BunnyException to catch invalid http responses.
    /// </summary>
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
    /// <summary>
    /// InvalidRequest BunnyException to catch invalid http requests.
    /// </summary>
    public class BunnyInvalidRequestException : BunnyException
    {
        /// <summary>
        /// Invalid response exception used for invalid JSON returns
        /// </summary>
        /// <returns>A BunnyCDN InvalidResponse exception</returns>
        public BunnyInvalidRequestException() {}
        /// <summary>
        /// Invalid response exception used for invalid JSON returns
        /// </summary>
        /// <param name="message">Error message for base</param>
        /// <returns>A BunnyCDN InvalidResponse exception</returns>
        public BunnyInvalidRequestException(string message) : base(message) {}
    }
}