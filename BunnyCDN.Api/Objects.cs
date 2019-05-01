using System;

namespace BunnyCDN.Api
{
    public class BillingSummary
    {
        public double Balance { get; set; }
        public double ThisMonthCharges { get; set; }
        public BillingRecord[] BillingRecords { get; set; }
        public double MonthlyChargesStorage { get; set; }
        public double MonthlyChargesEUTraffic { get; set; }
        public double MonthlyChargesUSTraffic { get; set; }
        public double MonthlyChargesASIATraffic { get; set; }
        public double MonthlyChargesSATraffic { get; set; }
    }
    public class BillingRecord
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payer { get; set; }
        public string PaymentId { get; set; }
        public DateTime Timestamp { get { return utcTimestamp; } set { utcTimestamp = DateTime.SpecifyKind(value, DateTimeKind.Utc); } }
        private DateTime utcTimestamp;
        public bool InvoiceAvailable { get; set; }
        public BillingType Type { get; set; }
    }

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
        public DateTime LastChanged { get { return utcLastChanged; } set { utcLastChanged = DateTime.SpecifyKind(value, DateTimeKind.Utc); } }
        private DateTime utcLastChanged;
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
        public DateTime DateCreated { get { return utcDateCreated; } set { utcDateCreated = DateTime.SpecifyKind(value, DateTimeKind.Utc); } }
        private DateTime utcDateCreated;
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