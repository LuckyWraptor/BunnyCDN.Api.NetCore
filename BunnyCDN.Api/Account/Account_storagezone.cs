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
                        return await JsonWrapper.Deserialize<StorageZone[]>(httpResponse);
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
        /// <returns>The response StorageZone</returns>
        public async Task<StorageZone> CreateStorageZone(string zoneName)
        {
            if (!Regexes.StorageName.IsMatch(zoneName))
                throw new BunnyBadRequestException("Name may only contain alphabetical letters, numbers and dashes with a length of 3-20 characters.");

            using (HttpContent httpContent = new StringContent("{\"name\":\""+ zoneName +"\"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("storagezone"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.Created:
                        return await JsonWrapper.Deserialize<StorageZone>(httpResponse);
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Delete a StorageZone
        /// </summary>
        /// <param name="zoneId">Storage zone Identifier</param>
        /// <returns>Succes, throws if unauthorized</returns>
        public async Task<bool> DeleteStorageZone(long zoneId)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Zone Id must be higher than 0.");

            HttpResponseMessage httpResponse = await this.AccountKey.Client.DeleteAsync( GetPath("storagezone/"+ zoneId) );
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