using System;

namespace BunnyCDN.Api
{
    public class StorageEntry
    {
        public long FailIndex { get; set; }
        /// <summary>
        /// Storage object identification number
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Storage zone name
        /// </summary>
        public string StorageZoneName { get; set; }
        /// <summary>
        /// Object location path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Object name
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// Content length
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// Object changed timestamp (UTC)
        /// </summary>
        public DateTime LastChanged { get; set; }
        /// <summary>
        /// Object is directory
        /// </summary>
        public bool IsDirectory { get; set; }
        /// <summary>
        /// Server identifier storing the object
        /// </summary>
        public uint ServerId { get; set; }
        /// <summary>
        /// User identifier owning the object
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Object creation timestamp (UTC)
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Storage zone identification number
        /// </summary>
        public long StorageZoneId { get; set; }
    }

    public class ErrorMessage
    {
        /// <summary>
        /// Http StatusCode (Always the same as the HTTP response StatusCode)
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}