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
    public class Account_requests_storagezone
    {
        [Fact]
        public async void Account_GetStorageZones_valid()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/storagezone").Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(CorrectStorageZones));

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act
            StorageZone[] zones = await account.GetStorageZones();

            // Assert
            Assert.Equal(CorrectStorageZones.Length, zones.Length);
            Assert.Equal(CorrectStorageZones[0].Id, zones[0].Id);
        }

        [Fact]
        public async void Account_GetStorageZones_unauthorized()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/storagezone").Respond(HttpStatusCode.Unauthorized);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);
            
            // Act & Arrange
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => { await account.GetStorageZones(); });
        }

        [Theory]
        [InlineData("{")]
        [InlineData("{}")]
        [InlineData("[")]
        [InlineData("[}")]
        public async void Account_GetStorageZones_invalid(string jsonString)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/storagezone").Respond(HttpStatusCode.OK, "application/json", jsonString);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);
            
            // Act & Arrange
            await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => { await account.GetStorageZones(); });
        }

        [Fact]
        public async void Account_CreateStorageZone_valid()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/storagezone").Respond(HttpStatusCode.OK);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            Assert.True(await account.CreateStorageZone("validName"));
        }

        [Theory]
        [InlineData("12")]
        [InlineData("1234567890123456789012345")]
        [InlineData("asd$")]
        [InlineData("asd$-234")]
        public async void Account_CreateStorageZone_invalid(string zoneName)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/storagezone").Respond(HttpStatusCode.OK);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            BunnyBadRequestException exception = await Assert.ThrowsAsync<BunnyBadRequestException>( async() => {await account.CreateStorageZone(zoneName);} );
            Assert.Equal("Name may only contain alphabetical letters, numbers and dashes with a length of 3-20 characters.", exception.Message);
        }

        [Fact]
        public async void Account_CreateStorageZone_unauthorized()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Post, "*/storagezone").Respond(HttpStatusCode.Unauthorized);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await account.CreateStorageZone("validName");} );
        }


        private static StorageZone[] CorrectStorageZones = new StorageZone[] {
            new StorageZone() {
                Id = 1,
                UserId = "17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481",
                Name = "storageName1",
                Password = "password",
                ReadOnlyPassword = "readonlypassword",
                DateModified = DateTime.UtcNow,
                Deleted = false,
                FilesStored = 5,
                StorageUsed = 20480
            },
            new StorageZone() {
                Id = 2,
                UserId = "17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481",
                Name = "storageName2",
                Password = "password",
                ReadOnlyPassword = "readonlypassword",
                DateModified = DateTime.UtcNow,
                Deleted = false,
                FilesStored = 10,
                StorageUsed = 40960
            },
            new StorageZone() {
                Id = 3,
                UserId = "17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481",
                Name = "storageName3",
                Password = "password",
                ReadOnlyPassword = "readonlypassword",
                DateModified = DateTime.UtcNow,
                Deleted = false,
                FilesStored = 10,
                StorageUsed = 40960
            },
        };
    }
}