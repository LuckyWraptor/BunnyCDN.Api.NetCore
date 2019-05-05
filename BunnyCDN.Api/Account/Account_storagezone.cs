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
        /// Retrieves all the account's storagezones
        /// </summary>
        /// <returns>An array of StorageZones</returns>
        public async Task<StorageZone[]> GetStorageZones()
        {
            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath("storagezone") );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();

                    StorageZone[] summary;
                    try {
                        summary = JsonConvert.DeserializeObject<StorageZone[]>(jsonString);
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
        /// Creates a new storage zone.
        /// </summary>
        /// <param name="zoneName">Storage zone name (alphabetical letters, numbers and dashes with 3-20 characters in length)</param>
        /// <returns></returns>
        public async Task<bool> CreateStorageZone(string zoneName)
        {
            if (!Regexes.StorageName.IsMatch(zoneName))
                throw new BunnyBadRequestException("Name may only contain alphabetical letters, numbers and dashes with a length of 3-20 characters.");

            using (HttpContent httpContent = new StringContent("{\"name\":\""+ zoneName +"\"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("storagezone"), httpContent );
                switch(httpResponse.StatusCode)
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
}