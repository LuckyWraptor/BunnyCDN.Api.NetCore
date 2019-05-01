using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    public class Account
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
    
        public async Task<BillingSummary> GetBillingSummary()
        {
            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath("billing") );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();

                    BillingSummary summary;
                    try {
                        summary = JsonConvert.DeserializeObject<BillingSummary>(jsonString);
                    } catch (JsonException) {
                        throw new BunnyInvalidResponseException();
                    }
                    
                    if (summary == null)
                        throw new BunnyInvalidResponseException();
                    return summary;
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
        internal string GetPath(string path) => $"{Variables.BaseApiUrl}{path}";
    }
}