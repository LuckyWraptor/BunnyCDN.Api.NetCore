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
    public class Storage_requests
    {
        private readonly Storage storage;
        public Storage_requests()
        {
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/testing/testfile").Respond("application/octet-stream", "Yep, this is the testfile");
            mockHttp.When(HttpMethod.Put, "*/testing/testfile").Respond(HttpStatusCode.Created);
            mockHttp.When(HttpMethod.Delete, "*/testing/testfile").Respond(HttpStatusCode.OK);

            mockHttp.When(HttpMethod.Get, "*/testing/testfolder/").Respond("application/json", JsonConvert.SerializeObject(FolderStorageEntries));


            StorageKey sKey = new StorageKey();
            sKey.SetToken("12345678-1234-1234-123456789012-1234-1234", mockHttp);
            
            storage = new Storage(sKey, "testing");
        }

        [Fact]
        public async void Storage_GetFile_valid()
        {
            byte[] result = await storage.GetFile("testfile");

            Assert.Equal(Encoding.UTF8.GetBytes("Yep, this is the testfile"), result);
        }

        [Fact]
        public async void Storage_GetFile_unauthorized()
        {
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await storage.GetFile("throwunauthorized");} );
        }

        [Fact]
        public async void Storage_GetFile_notfound()
        {
            await Assert.ThrowsAsync<BunnyNotFoundException>( async() => {await storage.GetFile("thrownotfound");} );
        }

        [Theory]
        [InlineData("testfolder")]
        [InlineData("testfolder/")]
        public async void Storage_GetFolder_valid(string testFolder)
        {
            StorageEntry[] storageEntries = await storage.GetFolder(testFolder);

            Assert.Equal(3, storageEntries.Length);
        }

        [Fact]
        public async void Storage_GetFolder_unauthorized()
        {
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await storage.GetFolder("throwunauthorized");} );
        }

        [Fact]
        public async void Storage_GetFolder_notfound()
        {
            await Assert.ThrowsAsync<BunnyNotFoundException>( async() => {await storage.GetFolder("thrownotfound");} );
        }

        [Fact]
        public async void Storage_GetFolder_badrequest()
        {
            BunnyBadRequestException exception = await Assert.ThrowsAsync<BunnyBadRequestException>( async() => {await storage.GetFolder("throwbadrequest");} );

            Assert.Equal(MockTools.BadRequestError.Message, exception.Message);
        }

        [Theory]
        [InlineData("throwinvalidbadrequest")]
        [InlineData("throwinvalidbadrequest1")]
        [InlineData("throwinvalidbadrequest2")]
        public async void Storage_GetFolder_badrequest_invalidResponse(string path)
        {
            BunnyBadRequestException exception = await Assert.ThrowsAsync<BunnyBadRequestException>( async() => {await storage.GetFolder(path);} );

            Assert.Equal("No response error provided.", exception.Message);
        }

        [Theory]
        [InlineData("/throwemptyok/")]
        [InlineData("/throwinvalidokjson/")]
        public async void Storage_GetFolder_invalidResponse(string path)
        {
            BunnyInvalidResponseException exception = await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => {await storage.GetFolder(path);} );
        }


        [Fact]
        public async void Storage_Put_valid()
        {
            Assert.True(await storage.Put(Encoding.UTF8.GetBytes("Content"), "testfile"));
        }

        [Fact]
        public async void Storage_Put_unauthorized()
        {
            await Assert.ThrowsAsync<BunnyUnauthorizedException>(async() => {await storage.Put(Encoding.UTF8.GetBytes("Content"), "throwunauthorized");} );
        }

        [Theory]
        [InlineData("")]
        [InlineData("throwbadrequest")]
        public async void Storage_Put_failed(string path)
        {
            Assert.False(await storage.Put(Encoding.UTF8.GetBytes("Content"), path));
        }


        [Fact]
        public async void Storage_Delete_valid()
        {
            Assert.True(await storage.Delete("testfile"));
        }

        [Fact]
        public async void Storage_Delete_unauthorized()
        {
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => { await storage.Delete("throwunauthorized"); });
        }

        [Theory]
        [InlineData("")]
        [InlineData("throwbadrequest")]
        public async void Storage_Delete_failed(string path)
        {
            Assert.False(await storage.Delete(path));
        }


        private static readonly Guid UserGuid = Guid.Parse("27f8f894-d397-4adb-88ee-b56b84c4f66b");
        private static readonly StorageEntry[] FolderStorageEntries = new StorageEntry[] {
            new StorageEntry() {
                FailIndex = 0,
                Guid = Guid.Parse("66220219-0789-4a20-aab0-e9b6d6f44784"),
                StorageZoneName = "testing",
                Path = "/testing/testfolder/",
                ObjectName = "testfile1",
                Length = 1650,
                LastChanged = DateTime.Parse("2019-04-30T15:01:29.052Z"),
                ServerId = 69,
                IsDirectory = false,
                UserId = UserGuid,
                DateCreated = DateTime.Parse("2019-04-29T15:01:29.052Z"),
                StorageZoneId = 10
            },
            new StorageEntry() {
                FailIndex = 0,
                Guid = Guid.Parse("66220219-0789-4a20-aab0-e9b6d6f44785"),
                StorageZoneName = "testing",
                Path = "/testing/testfolder/",
                ObjectName = "testfile2",
                Length = 1099,
                LastChanged = DateTime.Parse("2019-04-30T15:01:29.052Z"),
                ServerId = 69,
                IsDirectory = false,
                UserId = UserGuid,
                DateCreated = DateTime.Parse("2019-04-29T15:01:29.052Z"),
                StorageZoneId = 10
            },
            new StorageEntry() {
                FailIndex = 0,
                Guid = Guid.Parse("66220219-0789-4a20-aab0-e9b6d6f44786"),
                StorageZoneName = "testing",
                Path = "/testing/testfolder/",
                ObjectName = "testfolder1",
                Length = 0,
                LastChanged = DateTime.Parse("2019-04-30T15:01:29.052Z"),
                ServerId = 0,
                IsDirectory = false,
                UserId = UserGuid,
                DateCreated = DateTime.Parse("2019-04-29T15:01:29.052Z"),
                StorageZoneId = 10
            }
        };
    }
}