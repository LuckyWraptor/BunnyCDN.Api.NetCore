using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{

    /// <summary>
    /// Account API interface
    /// </summary>
    public partial class Account : AccountInterface
    {
        /// <summary>
        /// AccountKey, used to retrieve the required HttpClient
        /// </summary>
        private readonly AccountKey AccountKey;

        /// <summary>
        /// Account API Interface
        /// </summary>
        /// <param name="accKey">AccountKey token-provider</param>
        public Account(AccountKey accKey)
        {
            if (accKey == null || string.IsNullOrWhiteSpace(accKey.Token))
                throw new BunnyTokenException("Invalid AccountKey provided!");

            this.AccountKey = accKey;
        }

        /// <summary>
        /// Account API Interface
        /// </summary>
        /// <param name="accountKey"></param>
        public Account(string accountKey)
        {
            if (string.IsNullOrWhiteSpace(accountKey))
                throw new BunnyTokenException("No Account token provided!");

            this.AccountKey = new AccountKey(accountKey);
        }

        /// <summary>
        /// Retrieve the billing summary from the account.
        /// </summary>
        /// <returns>BillingSummary containing all the returned data, throws upon failure</returns>
        public async Task<BillingSummary> GetBillingSummary()
        {
            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath("billing") );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await JsonWrapper.Deserialize<BillingSummary>(httpResponse);
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }
        }

        /// <summary>
        /// Attempts to validate a coupon on your account.
        /// </summary>
        /// <param name="couponCode">Coupon code to validate</param>
        /// <returns>Success state or throws</returns>
        public async Task<bool> ApplyCoupon(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
                throw new BunnyBadRequestException("The presented coupon cannot be empty/null");
            couponCode = Uri.EscapeUriString(couponCode);

            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath($"billing/applycode?couponCode={couponCode}") );
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                default:
                    return false;
            }
        }

        /// <summary>
        /// Retrieves the statistics from the account based of the provided criteria, last month if no datespan was provided
        /// </summary>
        /// <param name="dtFrom">(Optional) From date</param>
        /// <param name="dtTo">(Optional) To Date</param>
        /// <param name="pullZone">(Optional) PullZone id</param>
        /// <param name="serverZoneId">(Optional) ServerZoneId</param>
        /// <returns>The account's statistic summary</returns>
        public async Task<StatisticSummary> GetStatisticSummary(DateTime? dtFrom = null, DateTime? dtTo = null, long? pullZone = null, long? serverZoneId = null)
        {
            Uri requestUri = new Uri( GetPath("statistics") );
            if (dtFrom.HasValue || dtTo.HasValue)
            {
                if ((!dtFrom.HasValue || dtFrom.Value == DateTime.MinValue)
                || (!dtTo.HasValue || dtTo.Value == DateTime.MinValue))
                    throw new BunnyBadRequestException("Both 'from' and 'to' dates must be provided");

                if (dtTo < dtFrom)
                {
                    // Switch around if not in the correct order.
                    var dtNewTo = dtFrom;
                    dtFrom = dtTo;
                    dtTo = dtNewTo;
                }

                requestUri = requestUri
                    .AddParameter("dateFrom", dtFrom.Value.ToString("yyyy-MM-dd"))
                    .AddParameter("dateTo", dtTo.Value.ToString("yyyy-MM-dd"));
            }

            if (pullZone.HasValue)
                requestUri = requestUri.AddParameter("pullZone", pullZone.Value.ToString());
            if (serverZoneId.HasValue)
                requestUri = requestUri.AddParameter("serverZoneId", serverZoneId.Value.ToString());

            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync(requestUri.ToString());
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await JsonWrapper.Deserialize<StatisticSummary>(httpResponse);
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }
        }


        /// <summary>
        /// Gets a valid API URL string.
        /// </summary>
        /// <param name="path">Input path</param>
        /// <returns>A valid URL for API calls</returns>
        private string GetPath(string path) => $"{Variables.BaseApiUrl}{path}";
    }
}