using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    public partial class Account
    {
        /// <summary>
        /// Purge a file based off th url provided
        /// </summary>
        /// <param name="urlString">Purge url</param>
        /// <returns>Success, throws if unauthorized/invalid</returns>
        public async Task<bool> PurgeUrl(string urlString)
        {
            if (string.IsNullOrWhiteSpace(urlString))
                throw new BunnyBadRequestException("No url provided.");

            Uri requestUri;
            try {
                requestUri = new Uri(urlString);
            } catch {
                throw new BunnyBadRequestException("Invalid url provided.");
            }

            Dictionary<string, string> formUrls = new Dictionary<string, string>() {
                { "urls", requestUri.ToString() }
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(formUrls))
            {
                return await completePurgeRequest("purge", httpContent);
            }
        }

        /// <summary>
        /// Purges all files in a pull zone
        /// </summary>
        /// <param name="pullZoneId">Pull zone id</param>
        /// <returns>Success, throws if unauthorized</returns>
        public async Task<bool> PurgePullZone(long pullZoneId)
        {
            return await completePurgeRequest($"pullzone/{pullZoneId}/purgeCache");
        }


        private async Task<bool> completePurgeRequest(string path, HttpContent httpContent = null)
        {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath(path), httpContent );
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
    }
}