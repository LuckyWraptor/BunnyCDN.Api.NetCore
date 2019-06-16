using System;
using System.Net.Http;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    /// <summary>
    /// AccessKey for API authorization and request handling.
    /// </summary>
    public class AccessKey : IDisposable
    {
        /// <summary>
        /// API Token required for successful REST calls
        /// </summary>
        /// <value>API Token string</value>
        public string Token { get; protected set; }
        /// <summary>
        /// HttpClient configured with Token header.
        /// </summary>
        /// <value>Preconfigured HttpClient using the token.</value>
        public HttpClient Client { get; protected set; }

        /// <summary>
        /// Access token provider instance
        /// </summary>
        public AccessKey() { }

        /// <summary>
        /// Access token provider instance
        /// </summary>
        /// <param name="apiKey">Access token</param>
        public AccessKey(string apiKey)
        {
            SetToken(apiKey);
        }

        /// <summary>
        /// Sets the token for the key provider
        /// </summary>
        /// <param name="apiKey">API Token</param>
        /// <param name="httpMessageHandler">The HttpMessageHandler instance to handle the token requests</param>
        public void SetToken(string apiKey, HttpMessageHandler httpMessageHandler = null)
        {
            this.Token = apiKey;
            
            HttpClient httpClient;
            if (httpMessageHandler != null)
                httpClient = new HttpClient(httpMessageHandler);
            else
                httpClient = new HttpClient();


            httpClient.DefaultRequestHeaders.Add("AccessKey", this.Token);
            this.Client = httpClient;
        }

        /// <summary>
        /// Dispose the AccessKey
        /// </summary>
        public void Dispose()
        {
            if (Client != null)
                Client.Dispose();
            Token = null;
        }
    }

    /// <summary>
    /// AccountKey for Account-specific API authorization and request handling.
    /// </summary>
    public class AccountKey : AccessKey
    {
        /// <summary>
        /// Account Token provider
        /// </summary>
        public AccountKey() {}

        /// <summary>
        /// Account Token provider
        /// </summary>
        /// <param name="apiKey">API Token</param>
        public AccountKey(string apiKey) : base(apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey) || !Regexes.AccountToken.IsMatch(apiKey))
                throw new BunnyTokenException("Account token is invalid!", apiKey);
        }
    }

    /// <summary>
    /// StorageKey for Storage-specific API authorization and request handling.
    /// </summary>
    public class StorageKey : AccessKey
    {
        /// <summary>
        /// Storage Token provider
        /// </summary>
        public StorageKey() { }

        
        /// <summary>
        /// Storage Token provider
        /// </summary>
        /// <param name="apiKey">API Token</param>
        public StorageKey(string apiKey) : base(apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey) || !Regexes.StorageToken.IsMatch(apiKey))
                throw new BunnyTokenException("Storage token is invalid!", apiKey);
        }
    }
}
