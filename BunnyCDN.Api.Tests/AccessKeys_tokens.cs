using System;
using System.Collections.Generic;
using Xunit;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class AccessKeys_tokens
    {
        [Theory]
        [MemberData(nameof(validAccountKeys))]
        public void AccountKey_Valid_token(string apiKey)
        {
            AccountKey accKey = new AccountKey(apiKey);
            Assert.Equal(apiKey, accKey.Token);
        }
        [Theory]
        [MemberData(nameof(invalidAccountKeys))]
        public void AccountKey_Invalid_token(string apiKey)
        {
            AccountKey accKey = null;
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>(() => { accKey = new AccountKey(apiKey); });

            Assert.Equal(apiKey, exception.Token);
            Assert.Equal("Account token is invalid!", exception.Message);
            Assert.Null(accKey);
        }

        [Theory]
        [MemberData(nameof(validStorageKeys))]
        public void StorageKey_Valid_token(string apiKey)
        {
            StorageKey sKey = new StorageKey(apiKey);
            Assert.Equal(apiKey, sKey.Token);
        }

        [Theory]
        [MemberData(nameof(invalidStorageKeys))]
        public void StorageKey_Invalid_token(string apiKey)
        {
            StorageKey sKey = null;
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>(() => { sKey = new StorageKey(apiKey); });

            Assert.Equal(apiKey, exception.Token);
            Assert.Equal("Storage token is invalid!", exception.Message);
            Assert.Null(sKey);
        }

        public static IEnumerable<object[]> validAccountKeys => new List<object[]> {
            new object[] { "01234567-0123-0123-0123-01234567890123456789-0123-0123-0123-012345678901" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "d1f6feEb-8B85-3Ad2-faAc-eEfbed79C1a3cdcDfdbB-ABF0-FBc1-ff25-fFffc82feEbf" },
            new object[] { "DeeE4Da6-f77c-e1B6-Deee-1aFeDf985Aa25e827CC5-6dD7-FD5E-DFA1-E2f5efe6801d" },
            new object[] { "9e0cbb1D-f787-F5ed-33FD-84a6f9CE1BBA0B4471BD-8DAf-b747-1cab-bDee560d41fE" },
            new object[] { "DAEdaEEf-886B-6C71-8bcC-7C408c396FaEaD3bC600-d0ad-F52B-52FD-a46f075D1633" },
            new object[] { "B065F65F-2150-e045-d9E7-286C7dF5BBAD27Ab607c-ecf8-A27d-FD03-2DdCFC1bC3A6" },
        };
        public static IEnumerable<object[]> invalidAccountKeys => new List<object[]> {
            new object[] { "1234567-1234-1234-1234-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "12345678-123-1234-1234-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "12345678-1234-123-1234-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "12345678-1234-1234-123-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "12345678-1234-1234-1234-1234567890123456789-1234-1234-1234-123456789012" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-123-1234-1234-123456789012" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-1234-123-1234-123456789012" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-1234-1234-123-123456789012" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-1234-1234-1234-12345678901" },
            new object[] { "12345678-1234-1234-1234--1234-1234-1234-123456789012" },
            new object[] { "123g5678-1234-1234-12345678901234567890-1234-1234-1234-123456789012" },
            new object[] { "12345678-1234-1234-1234-12345678901234567890-1234-1234-1234-12345678g2" },
        };

        public static IEnumerable<object[]> validStorageKeys => new List<object[]> {
            new object[] { "12345678-1234-1234-123456789012-1234-1234" },
            new object[] { "01234567-0123-0123-012345678901-0123-0123" },
            new object[] { "ac13ce5F-fc37-f152-9FBcbB8c6d2C-fB5e-Cc03" },
            new object[] { "0138DE1E-aBE4-1Fbf-7CB9DcE1DdAc-7Eb2-95EA" },
            new object[] { "0aE8D5e9-a45F-9cAe-Ca4efa3eA6ED-d1C3-60Eb" },
            new object[] { "c3EaE2AC-f155-F2Eb-1220Af7da7Bd-6aAA-19Bd" },
            new object[] { "da80F7D1-EE1A-Cbff-dcb71DDfbc45-CC9a-B4fE" },
        };
        public static IEnumerable<object[]> invalidStorageKeys => new List<object[]> {
            new object[] { "1234567-1234-1234-12356789012-1234-1234" },
            new object[] { "12345678-123-1234-12356789012-1234-1234" },
            new object[] { "12345678-1234-123-12356789012-1234-1234" },
            new object[] { "12345678-1234-1234-1235678901-1234-1234" },
            new object[] { "12345678-1234-1234-12356789012-123-1234" },
            new object[] { "12345678-1234-1234-12356789012-1234-123" },
            new object[] { "012g567-0123-0123-01234568901-0123-0123" },
            new object[] { "12345678-1234-1234-123456789012-1234-123g" },
            new object[] { "12345678-1234-1234--1234-1234" },
        };
    }
}
