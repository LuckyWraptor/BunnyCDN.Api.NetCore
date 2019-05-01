using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

using Newtonsoft.Json;
using RichardSzalay.MockHttp;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class Account_base
    {
       

        [Theory]
        [MemberData(nameof(InvalidAccountKeys))]
        public void Account_Invalid_StorageKey(AccountKey accKey)
        {
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>( () => {new Account(accKey);} );
            
            Assert.Equal("Invalid AccountKey provided!", exception.Message);
        }

        [Theory]
        [MemberData(nameof(EmptyAccountTokens))]
        public void Storage_Invalid_Token(string accKey)
        {
            BunnyTokenException exception = Assert.Throws<BunnyTokenException>( () => {new Account(accKey);} );
            
            Assert.Equal("No Account token provided!", exception.Message);
        }


        public static IEnumerable<object[]> EmptyAccountTokens => new List<object[]> {
            new object[] { null },
            new object[] { "" },
            new object[] { "   " }
        };
        public static IEnumerable<object[]> InvalidAccountKeys => new List<object[]>() {
            new object[] { null },
            new object[] { new AccountKey() } 
        };
    }   
}