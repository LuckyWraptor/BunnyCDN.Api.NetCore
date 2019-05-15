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

    public class EdgeRule
    {
        public Guid Guid { get; set; }
        public int ActionType { get; set; }
        public string ActionParameter1 { get; set; }
        public string ActionParameter2 { get; set; }
        public EdgeRuleTrigger[] Triggers { get; set; }
        public int TriggerMatchingType { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }
    public class EdgeRuleTrigger
    {
        public int Type { get; set; }
        public string[] PatternMatches { get; set; }
        public int PatternMatchingType { get; set; }
        public string Parameter1 { get; set; }
    }
    public class ErrorMessage
    {
        /// <summary>
        /// Invalid field name
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// ErrorKey string
        /// </summary>
        public string ErrorKey { get; set; }
        /// <summary>
        /// Http StatusCode (Always the same as the HTTP response StatusCode)
        /// </summary>
        public int HttpCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }

    public class Hostname
    {
        /// <summary>
        /// Hostname identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The full hostname domain value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// True if the hostname will force the users to use a SSL connection
        /// </summary>
        public bool ForceSSL { get; set; }
        /// <summary>
        /// True if the hostname is a system hostname
        /// </summary>
        public bool IsSystemHostname { get; set; }
        /// <summary>
        /// True if the hostname is configured with an SSL certificate
        /// </summary>
        public bool HasCertificate { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class PullZone
    {
        /// <summary>
        /// The unique ID of the pull zone
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the pull zone
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The origin URL where the pull zone files are pulled from.
        /// </summary>
        public string OriginUrl { get; set; }
        /// <summary>
        /// The owning account identifier
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// True if the pull zone is currently enabled and running
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// The list of hostnames linked to the Pull Zone
        /// </summary>
        public Hostname[] Hostnames { get; set; }
        /// <summary>
        /// The ID of the storage zone that the pull zone is linked to. If the value is < 1, it means the zone is not connected to a storage zone.
        /// </summary>
        public long StorageZoneId { get; set; }
        /// <summary>
        /// The list of referrer hostnames that are allowed to access the pull zone. Requests containing the header Referer hostname that is not on the list will be rejected. If empty, all the referrers are allowed
        /// </summary>
        public string[] AllowedReferrers { get; set; }
        /// <summary>
        /// The list of Referrers that are blocked from accessing the pull zone.
        /// </summary>
        public string[] BlockedReferrers { get; set; }
        /// <summary>
        /// The list of IPs that are blocked from accessing the pull zone. Requests coming from the following IPs will be rejected. If empty, all the IPs will be allowed
        /// </summary>
        public string[] BlockedIps { get; set; }
        /// <summary>
        /// True if the delivery from the US zone is enabled for this pull zone
        /// </summary>
        public bool EnableGeoZoneUS { get; set; }
        /// <summary>
        /// True if the delivery from the EU zone is enabled for this pull zone
        /// </summary>
        public bool EnableGeoZoneEU { get; set; }
        /// <summary>
        /// True if the delivery from the ASIA zone is enabled for this pull zone
        /// </summary>
        public bool EnableGeoZoneASIA { get; set; }
        /// <summary>
        /// True if the delivery from the South American zone is enabled for this pull zone
        /// </summary>
        public bool EnableGeoZoneSA { get; set; }
        /// <summary>
        /// True if the delivery from the African zone is enabled for this pull zone
        /// </summary>
        public bool EnableGeoZoneAF { get; set; }
        /// <summary>
        /// True if the URL secure token authentication security is enabled
        /// </summary>
        public bool ZoneSecurityEnabled { get; set; }
        /// <summary>
        /// The security key used for secure URL token authentication
        /// </summary>
        public Guid ZoneSecurityKey { get; set; }
        /// <summary>
        /// True if the zone security hash should include the remote IP
        /// </summary>
        public bool ZoneSecurityIncludeHashRemoteIP { get; set; }
        /// <summary>
        /// True if the Pull Zone is ignoring query strings when serving cached objects
        /// </summary>
        public bool IgnoreQueryStrings { get; set; }
        /// <summary>
        /// The monthly limit of bandwidth in bytes that the pullzone is allowed to use. Set to 0 for unlimited.
        /// </summary>
        public long MonthlyBandwidthLimit { get; set; }
        /// <summary>
        /// The amount of bandwidth in bytes that the pull zone used this month
        /// </summary>
        public long MonthlyBandwidthUsed { get; set; }
        /// <summary>
        /// The total monthly charges generated by the pull zone this month.
        /// </summary>
        public double MonthlyCharges { get; set; }
        /// <summary>
        /// If true, when making an origin pull request a host header will be added with the current hostname.
        /// </summary>
        public bool AddHostHeader { get; set; }
        /// <summary>
        /// The pricing type of the pull zone. **Premium** = 0, **Volume** = 1
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// The custom nginx configuration set for this pull zone.
        /// </summary>
        public string CustomNginxConfig { get; set; }
        /// <summary>
        /// The list of extensions that will add the access control origin header.
        /// </summary>
        public string AccessControlOriginHeaderExtensions { get; set; }
        /// <summary>
        /// Set to true if the CORS headers should be enabled
        /// </summary>
        public bool EnableAccessControlOriginHeader { get; set; }
        /// <summary>
        /// If true, the cookies are disabled for the pull zone.
        /// </summary>
        public bool DisableCookies { get; set; }
        /// <summary>
        /// The list of country codes that will get redirected to the absolute cheapest possible datacenter.
        /// </summary>
        public string[] BudgetRedirectedCountries { get; set; }
        /// <summary>
        /// The list of country codes that will be blocked from accessing the pull zone.
        /// </summary>
        public string[] BlockedCountries { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableOriginShield { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CacheControlMaxAgeOverride { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long BurstSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long RequestLimit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long BlockRootPathAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double CacheQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long LimitRatePerSecond { get; set; }
        /// <summary>
        /// Set to rate-limit after threshold.
        /// </summary>
        public long LimitRateAfter { get; set; }
        /// <summary>
        /// Set the connection limit per IP
        /// </summary>
        public long ConnectionLimitPerIPCount { get; set; }
        /// <summary>
        /// Price override?
        /// </summary>
        public double PriceOverride { get; set; }
        /// <summary>
        /// Set to true to add the Canonical header
        /// </summary>
        public bool AddCanonicalHeader { get; set; }
        /// <summary>
        /// Set to true if the logging for the zone should be enabled
        /// </summary>
        public bool EnableLogging { get; set; }
        /// <summary>
        /// Set to true to ignore the vary header
        /// </summary>
        public bool IgnoreVaryHeader { get; set; }
        /// <summary>
        /// Set to true if the to enable caching slices (Stream optimization)
        /// </summary>
        public bool EnableCacheSlice { get; set; }
        /// <summary>
        /// A list of edge rules for the pullzone
        /// </summary>
        public EdgeRule[] EdgeRules { get; set; }
        /// <summary>
        /// Set to true if the WebP vary feature should be enabled
        /// </summary>
        public bool EnableWebPVary { get; set; }
        /// <summary>
        /// Set to true if the Country vary feature should be enabled
        /// </summary>
        public bool EnableCountryCodeVary { get; set; }
        /// <summary>
        /// Set to true if the Mobile vary feature should be enabled
        /// </summary>
        public bool EnableMobileVary { get; set; }
        /// <summary>
        /// Set to true if the Hostname vary feature should be enabled
        /// </summary>
        public bool EnableHostnameVary { get; set; }
        /// <summary>
        /// CNAME domain
        /// </summary>
        public string CnameDomain { get; set; }
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
    public class StorageZone
    {
        /// <summary>
        /// The unique ID of the storage zone
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The unique user ID that owns the storage zone
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// The name of the storage zone
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The password for accessing the storage zone
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The read-only password for accessing the storage zone with read only access.
        /// </summary>
        public string ReadOnlyPassword { get; set; }
        /// <summary>
        /// Date of storage zone modification
        /// </summary>
        public DateTime DateModified { get { return utcDateModified; } set { utcDateModified = DateTime.SpecifyKind(value, DateTimeKind.Utc); } }
        private DateTime utcDateModified;
        /// <summary>
        /// The number of files stored in the storage zone
        /// </summary>
        public long FilesStored { get; set; }
        /// <summary>
        /// he amount of storage used by the storage zone in bytes
        /// </summary>
        public long StorageUsed { get; set; }
        public PullZone[] PullZones { get; set; }
        /// <summary>
        /// True if the storage is deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}