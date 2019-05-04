using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

using Newtonsoft.Json;
using RichardSzalay.MockHttp;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class Account_pullzone_purging
    {
        [Fact]
        public async void Account_PurgeUrl_valid()
        {
            // Arrange
            Account account = getValidAccount();

            // Act & Assert
            Assert.True(await account.PurgeUrl("https://test-url.b-cdn.net/testzone/testfile"));
        }

        [Fact]
        public async void Account_PurgeUrl_invalid()
        {
            // Arrange
            Account account = getInvalidAccount();

            // Act & Assert
            Assert.False(await account.PurgeUrl("https://invalid.b-cdn.net/invzone/invfile"));
        }

        [Theory]
        [InlineData("https:/test-url.b-cdn.net/testzone/testfile")]
        [InlineData("http://")]
        [InlineData("https:://domain")]
        public async void Account_PurgeUrl_invalidUrl(string url)
        {
            // Arrange
            Account account = getValidAccount();

            // Act & Assert
            BunnyBadRequestException exception = await Assert.ThrowsAsync<BunnyBadRequestException>( async() => {await account.PurgeUrl(url);});
            Assert.Equal("Invalid url provided.", exception.Message);
        }

        [Fact]
        public async void Account_PurgeUrl_unauthorized()
        {
            // Arrange
            Account account = getUnauthorizedAccount();

            // Act & Assert
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await account.PurgeUrl("https://fake.b-cdn.net/fakefile");});
        }

        [Fact]
        public async void Account_PurgePullZone_valid()
        {
            // Arrange
            Account account = getValidAccount();

            // Act & Assert
            Assert.True(await account.PurgePullZone(4861));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async void Account_PurgePullZone_invalid(long pullZoneId)
        {
            // Arrange
            Account account = getInvalidAccount();

            // Act & Assert
            Assert.False( await account.PurgePullZone(pullZoneId) );
        }

        [Fact]
        public async void Account_PurgePullZone_unauthorized()
        {
            // Arrange
            Account account = getUnauthorizedAccount();

            // Act & Assert
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await account.PurgePullZone(4861);} );
        }

        private Account getValidAccount()
        {
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/purge*").Respond(HttpStatusCode.OK);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            return new Account(accKey);
        }
        private Account getInvalidAccount()
        {
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/purge*").Respond(HttpStatusCode.BadRequest);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            return new Account(accKey);
        }
        private Account getUnauthorizedAccount()
        {
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/purge*").Respond(HttpStatusCode.Unauthorized);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            return new Account(accKey);
        }
    }
}