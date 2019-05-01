using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    public class Storage
    {
        /// <summary>
        /// Storagezone name set for the desired node
        /// </summary>
        /// <value>Storage zone name</value>
        public string Zone { get; protected set; }
        /// <summary>
        /// StorageKey, used to retrieve the required HttpClient
        /// </summary>
        private readonly StorageKey StorageKey;

        /// <summary>
        /// Storage API interface
        /// </summary>
        /// <param name="key">StorageKey token-provider</param>
        /// <param name="zone">Zone name for the desired node</param>
        public Storage(StorageKey sKey, string zone)
        {
            if (sKey == null || string.IsNullOrWhiteSpace(sKey.Token))
                throw new BunnyTokenException("Invalid StorageKey provided!");

            if (string.IsNullOrWhiteSpace(zone))
                throw new BunnyZoneException();

            this.StorageKey = sKey;
            this.Zone = zone;
        }

        /// <summary>
        /// Storage API interface
        /// </summary>
        /// <param name="storageToken">Token string</param>
        /// <param name="zone">Zone name for the desired node</param>
        public Storage(string storageToken, string zone)
        {
            if (string.IsNullOrWhiteSpace(storageToken))
                throw new BunnyTokenException("No Storage token provided!");

            if (string.IsNullOrWhiteSpace(zone))
                throw new BunnyZoneException();

            this.StorageKey = new StorageKey(storageToken);
            this.Zone = zone;
        }

        /// <summary>
        /// Retrieve a file from the storage API
        /// </summary>
        /// <param name="path">file path (without prefixing slash)</param>
        /// <returns>The file in a byte array, throws if failed</returns>
        public async Task<byte[]> GetFile(string path)
        {
            HttpResponseMessage httpResponse = await this.StorageKey.Client.GetAsync( GetPath(path) );
            byte[] content;
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    content = await httpResponse.Content.ReadAsByteArrayAsync();
                    break;
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                case HttpStatusCode.NotFound:
                    throw new BunnyNotFoundException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }
            return content;
        }

        /// <summary>
        /// Retrieve objects inside folder from the storage API (not-recursive)
        /// </summary>
        /// <param name="path">Path to retrieve objects from</param>
        /// <returns>StorageEntry array containing the objects</returns>
        public async Task<StorageEntry[]> GetFolder(string path)
        {
            if (!path.EndsWith("/"))
            {
                // Append slash to ensure folder retrieval.
                path += "/";
            }

            HttpResponseMessage httpResponse = await this.StorageKey.Client.GetAsync( GetPath(path) ); 
            StorageEntry[] storageEntries;
            string jsonString;
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    jsonString = await httpResponse.Content.ReadAsStringAsync();
                    storageEntries = JsonConvert.DeserializeObject<StorageEntry[]>(jsonString);
                    if (storageEntries == null)
                        throw new BunnyInvalidResponseException();
                    break;
                case HttpStatusCode.BadRequest:
                    jsonString = await httpResponse.Content.ReadAsStringAsync();
                    ErrorMessage error = JsonConvert.DeserializeObject<ErrorMessage>(jsonString);
                    if (error != null && error.Message != null)
                        throw new BunnyBadRequestException(error.Message);
                    throw new BunnyBadRequestException("No response error provided.");
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                case HttpStatusCode.NotFound:
                    throw new BunnyNotFoundException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }

            return storageEntries;
        }

        /// <summary>
        /// Creates/overwrites a file (and the missing path) in your storage zone.
        /// </summary>
        /// <param name="fileContent">File contents</param>
        /// <param name="path">Path to store file in zone</param>
        /// <returns>Success</returns>
        public async Task<bool> Put(byte[] fileContent, string path)
        {
            using (HttpContent httpContent = new ByteArrayContent(fileContent))
            {
                HttpResponseMessage httpResponse = await this.StorageKey.Client.PutAsync(GetPath(path), httpContent);
                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.Created:
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Deletes a file/directory-path from a the storage zone.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Success</returns>
        public async Task<bool> Delete(string path)
        {
            HttpResponseMessage httpResponse = await this.StorageKey.Client.DeleteAsync( GetPath(path) );
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
        /// Gets a valid API URL string.
        /// </summary>
        /// <param name="path">Input path</param>
        /// <returns>A valid URL for API calls</returns>
        internal string GetPath(string path) => $"{Variables.StorageUrl}{this.Zone}/{path}";
    }
}