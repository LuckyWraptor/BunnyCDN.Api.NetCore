using System;
using System.Net.Http;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    /// <summary>
    /// A token-key provider for API calls
    /// </summary>
    public class AccessKey : IDisposable
    {
        public string Token { get; protected set; }
        public HttpClient Client { get; protected set; }

        public AccessKey(string apiKey)
        {
            SetToken(apiKey);
        }

        /// <summary>
        /// Sets the token for the key provider
        /// </summary>
        /// <param name="apiKey">API Token</param>
        public void SetToken(string apiKey)
        {
            this.Token = apiKey;
            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("AccessKey", this.Token);
            this.Client = httpClient;
        }

        public void Dispose()
        {
            Client.Dispose();
            Token = null;
        }
    }
    /// AccountKey used for Account API calls
    public class AccountKey : AccessKey
    {
        public AccountKey(string apiKey) : base(apiKey)
        {
            if (!Regexes.AccountToken.IsMatch(apiKey))
                throw new BunnyTokenException("Account token is invalid!", apiKey);
        }
    }

    /// StorageKey used for Storage API calls
    public class StorageKey : AccessKey
    {
        public StorageKey(string apiKey) : base(apiKey)
        {
            if (!Regexes.StorageToken.IsMatch(apiKey))
                throw new BunnyTokenException("Storage token is invalid!", apiKey);
        }
    }
}
