using System;
using System.Net;
using System.Threading.Tasks;


namespace BunnyCDN.Api.UnitTests
{
    public class AccountTester : AccountInterface
    {
        public BillingSummary responseBillingSummary;
        public StatisticSummary responseStatisticsSummary;

        public StorageZone responseStorageZone;
        public StorageZone[] responseStorageZones;

        public PullZone responsePullZone;
        public PullZone[] responsePullZones;

        public bool responseBool;

        public async Task<BillingSummary> GetBillingSummary()
        {
            return await Task.Run(() => { return responseBillingSummary; });
        }

        public async Task<StatisticSummary> GetStatisticSummary(DateTime? dtFrom = null, DateTime? dtTo = null, long? pullZone = null, long? serverZoneId = null)
        {
            return await Task.Run(() => { return responseStatisticsSummary; });
        }

        public async Task<StorageZone[]> GetStorageZones()
        {
            return await Task.Run(() => { return responseStorageZones; });
        }

        public async Task<StorageZone> CreateStorageZone(string zoneName)
        {
            return await Task.Run(() => { return responseStorageZone; });
        }

        public async Task<PullZone[]> GetPullZones()
        {
            return await Task.Run(() => { return responsePullZones; });
        }

        public async Task<PullZone> GetPullZone(long zoneId)
        {
            return await Task.Run(() => { return responsePullZone; });
        }

        public async Task<PullZone> AddPullZone(string name, string originUrl, long? storageZoneId)
        {
            return await Task.Run(() => { return responsePullZone; });
        }


        public async Task<bool> ApplyCoupon(string couponCode)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> DeleteStorageZone(long zoneId)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> UpdatePullZone(long zoneId, PullZone pullZoneChages)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> DeletePullZone(long zoneId)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> AddEdgeRule(long zoneId, EdgeRule edgeRule)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> ModifyEdgeRule(long zoneId, EdgeRule edgeRule)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> DeleteEdgeRule(long zoneId, Guid guid)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> AddPullZoneHostname(long zoneId, string hostName)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> DeletePullZoneHostname(long zoneId, string hostName)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> SetPullZoneForceSSL(long zoneId, string hostName, bool forcing)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> LoadPullZoneCertificate(long zoneId, string hostName)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> UploadPullZoneCertificate(long zoneId, string hostName, string certificateBase64, string certificateKeyBase64)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PullZoneBlockIP(long zoneId, IPAddress blockedIP)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PullZoneBlockIP(long zoneId, string blockedIP)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PullZoneUnblockIP(long zoneId, IPAddress blockedIP)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PullZoneUnblockIP(long zoneId, string blockedIP)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PurgeUrl(string urlString)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> PurgePullZone(long pullZoneId)
        {
            return await Task.Run(() => { return responseBool; });
        }
    }
}