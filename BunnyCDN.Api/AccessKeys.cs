using System;

using BunnyCDN.Api.Internals;

namespace BunnyCDN.Api
{
    /// Generic AccessKey containing it's token.
    public class AccessKey
    {
        public string Token { get; protected set; }

        public AccessKey(string apiKey)
        {
            this.Token = apiKey;
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
