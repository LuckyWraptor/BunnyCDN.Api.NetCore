using System;
using System.Collections.Generic;
using Xunit;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class Storage_base
    {
        [Theory]
        [MemberData(nameof(InvalidStorageKeys))]
        public void Storage_Invalid_StorageKey(StorageKey sKey)
        {
            Storage storage;
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>( () => {storage = new Storage(sKey, "test");} );
            
            Assert.Equal("Invalid StorageKey provided!", exception.Message);
        }

        [Theory]
        [MemberData(nameof(EmptyStorageTokens))]
        public void Storage_Invalid_Token(string sKey)
        {
            Storage storage;
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>( () => {storage = new Storage(sKey, "test");} );
            
            Assert.Equal("No Storage token provided!", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Storage_Invalid_zone(string zone)
        {
            Storage storage;
            BunnyZoneException exception = Assert.Throws<BunnyZoneException>( () => {storage = new Storage("12345678-1234-1234-123456789012-1234-1234", zone);} );
        }

        public static IEnumerable<object[]> EmptyStorageTokens => new List<object[]> {
            new object[] { null },
            new object[] { "" },
            new object[] { "   " }
        };

        public static IEnumerable<object[]> InvalidStorageKeys = new List<object[]>() {
            new object[] { null },
            new object[] { new StorageKey() } 
        };
    }
}