using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    public partial class Account
    {
        /// <summary>
        /// Retrieve the PullZones allocated to your account.
        /// </summary>
        /// <returns>An array of the account PullZones</returns>
        public async Task<PullZone[]> GetPullZones()
        {
            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath("storagezone") );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                        return await JsonWrapper.Deserialize<PullZone[]>(httpResponse);
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                case HttpStatusCode.NotFound:
                    throw new BunnyNotFoundException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }
        }

        /// <summary>
        /// Create a new PullZone pointing to an origin or a StorageZone
        /// </summary>
        /// <param name="name">Name of the new PullZone</param>
        /// <param name="originUrl">Origin url to pull data from</param>
        /// <param name="storageZoneId">Storage Zone identifier (If using StorageZone)</param>
        /// <returns>The added PullZone</returns>
        public async Task<PullZone> AddPullZone(string name, string originUrl, long? storageZoneId)
        {
            if (!Regexes.PullZoneName.IsMatch(name))
                throw new BunnyBadRequestException("Invalid Zone name provided.");
            
            Uri uriString;
            if (!Uri.TryCreate(originUrl, UriKind.Absolute, out uriString))
                throw new BunnyBadRequestException("Invalid Origin Url provided.");
            

            PullZone pullZone = new PullZone() {
                Name = name,
                OriginUrl = uriString.ToString()
            };

            if (storageZoneId.HasValue)
            {
                if (storageZoneId.Value <= 0)
                    throw new BunnyBadRequestException("Invalid Storage Zone Identifier provided.");

                pullZone.StorageZoneId = storageZoneId.Value;
            }

            using (HttpContent httpContent = new StringContent(JsonWrapper.Serialize(pullZone)))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone"), httpContent );
                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.Created:
                        return await JsonWrapper.Deserialize<PullZone>(httpResponse);
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    default:
                        throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
                }
            }
        }


        /// <summary>
        /// Retrieve a specific PullZone from your account.
        /// </summary>
        /// <param name="zoneId">PullZone identifier to retrieve</param>
        /// <returns>The requested PullZone, throws otherwise</returns>
        public async Task<PullZone> GetPullZone(long zoneId)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync( GetPath("pullzone/"+ zoneId) );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                        return await JsonWrapper.Deserialize<PullZone>(httpResponse);
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                default:
                    throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
            }
        }

        /// <summary>
        /// Update a PullZone
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <param name="pullZoneChages">PullZone to apply</param>
        /// <returns>Success state, throws if failed</returns>
        public async Task<bool> UpdatePullZone(long zoneId, PullZone pullZoneChages)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            if (!Regexes.PullZoneName.IsMatch(pullZoneChages.Name))
                throw new BunnyBadRequestException("Invalid pullzone name provided.");

            Uri originUrl;
            if (!Uri.TryCreate(pullZoneChages.OriginUrl, UriKind.Absolute, out originUrl))
                throw new BunnyBadRequestException("Invalid Origin Url provided.");

            using (HttpContent httpContent = new StringContent(JsonWrapper.Serialize(pullZoneChages)))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone/"+ zoneId), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                            return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        throw new BunnyInvalidResponseException("Unexpected/unhandled response retrieved");
                }
            }
        }

        /// <summary>
        /// Deletes a PullZone (irreversable!)
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <returns>Success state, throws if failed.</returns>
        public async Task<bool> DeletePullZone(long zoneId)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            HttpResponseMessage httpResponse = await this.AccountKey.Client.DeleteAsync( GetPath("pullzone/"+ zoneId) );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return true;
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                case HttpStatusCode.BadRequest:
                    ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                    throw new BunnyBadRequestException(error);
                default:
                    return false;
            }
        }


        /// <summary>
        /// Adds an edge rule
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="edgeRule">Edge rule to add</param>
        /// <returns>Success, throws if failed</returns>
        public async Task<bool> AddEdgeRule(long zoneId, EdgeRule edgeRule)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            if (edgeRule.Guid != null && edgeRule.Guid != Guid.Empty)
                edgeRule.Guid = Guid.Empty;

            return await ModifyEdgeRule(zoneId, edgeRule);
        }

        /// <summary>
        /// Modifies an edge rule
        /// </summary>
        /// <param name="zoneId">Zone id to apply the rule to</param>
        /// <param name="edgeRule">Edge rule to modify</param>
        /// <returns>Success, throws if failed</returns>
        public async Task<bool> ModifyEdgeRule(long zoneId, EdgeRule edgeRule)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            using (HttpContent httpContent = new StringContent(JsonWrapper.Serialize(edgeRule)))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath($"pullzone/{zoneId}/edgerules/addorupdate"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.Created: // Returnes created for both updating and adding.
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Deletes an Edge rule (irreversable)
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="guid">Edge Rule identifier</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> DeleteEdgeRule(long zoneId, Guid guid)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            HttpResponseMessage httpResponse = await this.AccountKey.Client.DeleteAsync( GetPath($"pullzone/{zoneId}/edgerules/{guid}") );
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.NoContent: // Returnes created for both updating and adding.
                    return true;
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();                
                case HttpStatusCode.BadRequest:
                    ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                    throw new BunnyBadRequestException(error);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Add a Hostname to a provided PullZone
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="hostName">Hostname to remove</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> AddPullZoneHostname(long zoneId, string hostName)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            Uri hostUri;
            if (string.IsNullOrWhiteSpace(hostName) || !Uri.TryCreate(hostName, UriKind.Absolute, out hostUri))
                throw new BunnyBadRequestException("Invalid hostname provided.");

            using (HttpContent httpContent = new StringContent("{\"PullZoneId\":"+zoneId+",\"Hostname\":\""+hostUri+"\",}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone/addhostname"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.NoContent: // Returnes created for both updating and adding.
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();                
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }
        
        /// <summary>
        /// Removes a Hostname from a provided PullZone
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="hostName">Hostname to remove</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> DeletePullZoneHostname(long zoneId, string hostName)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            Uri hostUri;
            if (string.IsNullOrWhiteSpace(hostName) || !Uri.TryCreate(hostName, UriKind.Absolute, out hostUri))
                throw new BunnyBadRequestException("Invalid hostname provided.");

            Uri requestUri = new Uri( GetPath("pullzone/deletehostname") )
                .AddParameter("id", zoneId.ToString())
                .AddParameter("hostname", hostUri.ToString());


            HttpResponseMessage httpResponse = await this.AccountKey.Client.DeleteAsync( requestUri.ToString() );
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

        /// <summary>
        /// Set the forcing of a PullZone SSL
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="hostName">Hostname</param>
        /// <param name="forcing">Forcing state</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> SetPullZoneForceSSL(long zoneId, string hostName, bool forcing)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");

            Uri hostUri;
            if (string.IsNullOrWhiteSpace(hostName) || !Uri.TryCreate(hostName, UriKind.Absolute, out hostUri))
                throw new BunnyBadRequestException("Invalid hostname provided.");

            using (HttpContent httpContent = new StringContent("{\"PullZoneId\":"+zoneId+",\"Hostname\":\""+hostUri+"\",\"ForceSSL\":"+forcing.ToString()+"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath($"pullzone/setforcessl"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.Created:
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Request a Let's Encrypt certificate for a PullZone's specific hostname.
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="hostName">Hostname</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> LoadPullZoneCertificate(long zoneId, string hostName)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");
            
            Uri hostUri;
            if (string.IsNullOrWhiteSpace(hostName) || !Uri.TryCreate(hostName, UriKind.Absolute, out hostUri))
                throw new BunnyBadRequestException("Invalid hostname provided.");

            Uri requestUri = new Uri( GetPath("pullzone/loadfreecertificate") ).AddParameter("hostname", hostUri.ToString());

            HttpResponseMessage httpResponse = await this.AccountKey.Client.GetAsync(requestUri.ToString());
            switch(httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.Unauthorized:
                    throw new BunnyUnauthorizedException();
                case HttpStatusCode.BadRequest:
                    ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                    throw new BunnyBadRequestException(error);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Upload a custom certificate to for the PullZone's specific hostname
        /// </summary>
        /// <param name="zoneId">Zone identifier</param>
        /// <param name="hostName">Hostname</param>
        /// <param name="certificateBase64">Certificate (Base64-Encoded)</param>
        /// <param name="certificateKeyBase64">Certificate key (Base64-Encoded)</param>
        /// <returns>Success, throws otherwise</returns>
        public async Task<bool> UploadPullZoneCertificate(long zoneId, string hostName, string certificateBase64, string certificateKeyBase64)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");
            
            Uri hostUri;
            if (string.IsNullOrWhiteSpace(hostName) || !Uri.TryCreate(hostName, UriKind.Absolute, out hostUri))
                throw new BunnyBadRequestException("Invalid hostname provided.");

            if (!Regexes.Base64String.IsMatch(certificateBase64))
                throw new BunnyBadRequestException("Invalid Certificate provided, not a valid Base64 string");

            if (!Regexes.Base64String.IsMatch(certificateKeyBase64))
                throw new BunnyBadRequestException("Invalid Certificate Key provided, not a valid Base64 string");

            using (HttpContent httpContent = new StringContent("{\"PullZoneId\":"+zoneId+",\"Hostname\":\""+hostUri.ToString()+"\",\"Certificate\":\""+certificateBase64+"\",\"CertificateKey\":\""+certificateKeyBase64+"\"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone/addBlockedIp"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Blocks an IPAddress from the specified PullZone
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <param name="blockedIP">IPAddress to block</param>
        /// <returns>Succes, throws otherwise</returns>
        public async Task<bool> PullZoneBlockIP(long zoneId, IPAddress blockedIP)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");
            
            if (blockedIP == null || blockedIP.IsIPv6LinkLocal || blockedIP.IsIPv6SiteLocal)
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            
            string ipAddress;
            try {
                ipAddress = blockedIP.ToString();
            } catch (System.Net.Sockets.SocketException) {
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            }

            using (HttpContent httpContent = new StringContent("{\"PullZoneId\":"+zoneId+",\"BlockedIP\":\""+ipAddress+"\"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone/addcertificate"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Blocks an IP string from the specified PullZone
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <param name="blockedIP">IP to block</param>
        /// <returns>Succes, throws otherwise</returns>
        public async Task<bool> PullZoneBlockIP(long zoneId, string blockedIP)
        {
            if (string.IsNullOrWhiteSpace(blockedIP))
                throw new BunnyBadRequestException("Invalid IP string provided.");

            IPAddress ipAddress;
            try {
                ipAddress = IPAddress.Parse(blockedIP);
            } catch (FormatException) {
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            }
            
            return await PullZoneBlockIP(zoneId, ipAddress);
        }

        /// <summary>
        /// Removes a blocked IP from the specified PullZone
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <param name="blockedIP">IPAddress</param>
        /// <returns>Succes, throws otherwise</returns>
        public async Task<bool> PullZoneUnblockIP(long zoneId, IPAddress blockedIP)
        {
            if (zoneId <= 0)
                throw new BunnyBadRequestException("Invalid pullzone identifier provided.");
            
            if (blockedIP == null || blockedIP.IsIPv6LinkLocal || blockedIP.IsIPv6SiteLocal)
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            
            string ipAddress;
            try {
                ipAddress = blockedIP.ToString();
            } catch (System.Net.Sockets.SocketException) {
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            }

            using (HttpContent httpContent = new StringContent("{\"PullZoneId\":"+zoneId+",\"BlockedIP\":\""+ipAddress+"\"}"))
            {
                HttpResponseMessage httpResponse = await this.AccountKey.Client.PostAsync( GetPath("pullzone/removeBlockedIp"), httpContent );
                switch(httpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.Unauthorized:
                        throw new BunnyUnauthorizedException();
                    case HttpStatusCode.BadRequest:
                        ErrorMessage error = await JsonWrapper.Deserialize<ErrorMessage>(httpResponse);
                        throw new BunnyBadRequestException(error);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Removes a blocked IP string from the specified PullZone
        /// </summary>
        /// <param name="zoneId">PullZone identifier</param>
        /// <param name="blockedIP">IP to unblock</param>
        /// <returns>Succes, throws otherwise</returns>
        public async Task<bool> PullZoneUnblockIP(long zoneId, string blockedIP)
        {
            if (string.IsNullOrWhiteSpace(blockedIP))
                throw new BunnyBadRequestException("Invalid IP string provided.");

            IPAddress ipAddress;
            try {
                ipAddress = IPAddress.Parse(blockedIP);
            } catch (FormatException) {
                throw new BunnyBadRequestException("Invalid IPAddress provided.");
            }
            
            return await PullZoneUnblockIP(zoneId, ipAddress);
        }
    }
}