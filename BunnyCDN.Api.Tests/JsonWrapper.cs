using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Abstractions;

using Newtonsoft.Json;
using RichardSzalay.MockHttp;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class JsonWrapperTests
    {
        [Fact]
        public async void invalidJsonContent()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/test").Respond(HttpStatusCode.OK, "application/failed", "{}");

            HttpClient httpClient = mockHttp.ToHttpClient();

            // Act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("http://test.com/test");

            // Assert
            BunnyInvalidResponseException exception = await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => {await JsonWrapper.Deserialize<object>(httpResponse);} );        
        }

        [Fact]
        public async void validJsonContent()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/test").Respond(HttpStatusCode.OK, "application/json", "{\"Value\":\"Test\"}");

            HttpClient httpClient = mockHttp.ToHttpClient();

            // Act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("http://test.com/test");

            // Assert
            Assert.NotNull(await JsonWrapper.Deserialize<Hostname>(httpResponse));
        }

        [Theory]
        [InlineData("{")]
        [InlineData("[")]
        [InlineData("[}")]
        [InlineData("{}{}")]
        [InlineData("[}]")]
        public async void BadFormattedJsonContent(string jsonString)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/test").Respond(HttpStatusCode.OK, "application/failed", jsonString);

            HttpClient httpClient = mockHttp.ToHttpClient();

            // Act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("http://test.com/test");

            // Assert
            BunnyInvalidResponseException exception = await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => {await JsonWrapper.Deserialize<object>(httpResponse);} ); 
        }
    }
}