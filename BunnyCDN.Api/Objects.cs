using System;
using System.Collections.Generic;

namespace BunnyCDN.Api
{
    public class BillingSummary
    {
        /// <summary>
        /// The current account balance
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// The charges for the current month
        /// </summary>
        public double ThisMonthCharges { get; set; }
        /// <summary>
        /// The billing backlog
        /// </summary>
        public BillingRecord[] BillingRecords { get; set; }
        /// <summary>
        /// Current storage price for this month
        /// </summary>
        public double MonthlyChargesStorage { get; set; }
        /// <summary>
        /// Current traffic price for the European geozone
        /// </summary>
        public double MonthlyChargesEUTraffic { get; set; }
        /// <summary>
        /// Current traffic price for the Nort-American geozone
        /// </summary>
        public double MonthlyChargesUSTraffic { get; set; }
        /// <summary>
        /// Current traffic price for the Asian geozone
        /// </summary>
        public double MonthlyChargesASIATraffic { get; set; }
        /// <summary>
        /// Current traffic price for the South-African geozone
        /// </summary>
        public double MonthlyChargesSATraffic { get; set; }
    }
    public class BillingRecord
    {
        /// <summary>
        /// The unique ID of the billing record
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The amount referenced by the billing record
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// The identifier of the payer who paid for the billing.
        /// </summary>
        public string Payer { get; set; }
        /// <summary>
        /// The unique ID of the payment that the record was created for
        /// </summary>
        public string PaymentId { get; set; }
        /// <summary>
        /// The timestamp when the billing record was created
        /// </summary>
        public DateTime Timestamp { get { return utcTimestamp; } set { utcTimestamp = DateTime.SpecifyKind(value, DateTimeKind.Utc); } }
        private DateTime utcTimestamp;
        /// <summary>
        /// True if there is an invoice available for download for this record
        /// </summary>
        public bool InvoiceAvailable { get; set; }
        /// <summary>
        /// The type of the billing record. *Bitcoin* = 1, *CreditCard* = 2, *MonthlyUsage* = 3, *CouponCode* = 5
        /// </summary>
        public BillingType Type { get; set; }
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

    public class StatisticSummary
    {
        /// <summary>
        /// Total bandwidth used (in bytes).
        /// </summary>
        public long TotalBandwidthUsed { get; set; }
        /// <summary>
        /// Total reqests served
        /// </summary>
        public long TotalRequestsServed { get; set; }
        /// <summary>
        /// Current cache hit-rate
        /// </summary>
        public double CacheHitRate { get; set; }
        /// <summary>
        /// Total bandwidth usage with timestamps
        /// </summary>
        public Dictionary<DateTime, double> BandwidthUsedChart { get; set; }
        /// <summary>
        /// Cached bandwidth usage with timestamps
        /// </summary>
        public Dictionary<DateTime, double> BandwidthCachedChart { get; set; }
        /// <summary>
        /// Cache hit-rates with timestamps
        /// </summary>
        public Dictionary<DateTime, double> CacheHitRateChart { get; set; }
        /// <summary>
        /// Request serve count with timestamps
        /// </summary>
        public Dictionary<DateTime, double> RequestsServedChart { get; set; }
        /// <summary>
        /// Pull request count with timestamps
        /// </summary>

        public Dictionary<DateTime, double> PullRequestsPulledChart { get; set; }
        /// <summary>
        /// User balance with timestamps
        /// </summary>
        public Dictionary<DateTime, double> UserBalanceHistoryChart { get; set; }
        /// <summary>
        /// User storage usage with timestamps
        /// </summary>
        public Dictionary<DateTime, double> UserStorageUsedChart { get; set; }
        /// <summary>
        /// Geographical bandwidth usage within timespan (or last 30 days if unset)
        /// </summary>
        public Dictionary<string, long> GeoTrafficDistribution { get; set; }
        /// <summary>
        /// HTTP 3xx error count with timestamps
        /// </summary>
        public Dictionary<DateTime, double> Error3xxChart { get; set; }
        /// <summary>
        /// HTTP 4xx error count with timestamps
        /// </summary>
        public Dictionary<DateTime, double> Error4xxChart { get; set; }
        /// <summary>
        /// HTTP 5xx error count with timestamps
        /// </summary>
        public Dictionary<DateTime, double> Error5xxChart { get; set; }
    }

    public class StorageEntry
    {
        /// <summary>
        /// Use-case undocumented
        /// </summary>
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
}