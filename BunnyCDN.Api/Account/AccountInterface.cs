using System;
using System.Net;
using System.Threading.Tasks;

namespace BunnyCDN.Api
{
    /// <summary>
    /// Interface for the Account class
    /// </summary>
    public interface AccountInterface
    {
        Task<BillingSummary> GetBillingSummary();
        Task<bool> ApplyCoupon(string couponCode);
        Task<StatisticSummary> GetStatisticSummary(DateTime? dtFrom = null, DateTime? dtTo = null, long? pullZone = null, long? serverZoneId = null);

        // Storage zones
        Task<StorageZone[]> GetStorageZones();
        Task<StorageZone> CreateStorageZone(string zoneName);
        Task<bool> DeleteStorageZone(long zoneId);

        // Purging
        Task<bool> PurgeUrl(string urlString);
        Task<bool> PurgePullZone(long pullZoneId);

        // Pullzones
        Task<PullZone[]> GetPullZones();
        Task<PullZone> AddPullZone(string name, string originUrl, long? storageZoneId);
        Task<PullZone> GetPullZone(long zoneId);
        Task<bool> UpdatePullZone(long zoneId, PullZone pullZoneChages);
        Task<bool> DeletePullZone(long zoneId);
        Task<bool> AddEdgeRule(long zoneId, EdgeRule edgeRule);
        Task<bool> ModifyEdgeRule(long zoneId, EdgeRule edgeRule);
        Task<bool> DeleteEdgeRule(long zoneId, Guid guid);
        Task<bool> AddPullZoneHostname(long zoneId, string hostName);
        Task<bool> DeletePullZoneHostname(long zoneId, string hostName);
        Task<bool> SetPullZoneForceSSL(long zoneId, string hostName, bool forcing);
        Task<bool> LoadPullZoneCertificate(long zoneId, string hostName);
        Task<bool> UploadPullZoneCertificate(long zoneId, string hostName, string certificateBase64, string certificateKeyBase64);
        Task<bool> PullZoneBlockIP(long zoneId, IPAddress blockedIP);
        Task<bool> PullZoneBlockIP(long zoneId, string blockedIP);
        Task<bool> PullZoneUnblockIP(long zoneId, IPAddress blockedIP);
        Task<bool> PullZoneUnblockIP(long zoneId, string blockedIP);
    }
}