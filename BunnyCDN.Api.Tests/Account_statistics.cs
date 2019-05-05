using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

using Newtonsoft.Json;
using RichardSzalay.MockHttp;

using BunnyCDN.Api;

namespace BunnyCDN.Api.Tests
{
    public class Account_requests_statistics
    {
        [Fact]
        public async void Account_GetStatisticSummary_valid()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/statistics").Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(CorrectStatisticSummary));

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act
            StatisticSummary summary = await account.GetStatisticSummary();

            // Assert
            Assert.Equal(CorrectStatisticSummary.TotalBandwidthUsed, summary.TotalBandwidthUsed);
            Assert.Equal(CorrectStatisticSummary.BandwidthUsedChart.Count, summary.BandwidthUsedChart.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData("{")]
        [InlineData("{}{}")]
        [InlineData("[]")]
        public async void Account_GetStatisticSummary_invalid_response(string jsonString)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/statistics").Respond(HttpStatusCode.OK, "application/json", jsonString);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => {await account.GetStatisticSummary();} );
        }

        [Theory]
        [MemberData(nameof(InvalidDateInputs))]
        public async void Account_GetStatisticSummary_badrequest_date_one_invalid(DateTime dtFrom, DateTime dtTo)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/statistics").Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(CorrectStatisticSummary));

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            BunnyBadRequestException exception = await Assert.ThrowsAsync<BunnyBadRequestException>( async() => {await account.GetStatisticSummary(dtFrom, dtTo);} );
            Assert.Equal("Both 'from' and 'to' dates must be provided", exception.Message);
        }

        [Fact]
        public async void Account_GetStatisticSummary_unauthorized()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/statistics").Respond(HttpStatusCode.Unauthorized);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act & Assert
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => {await account.GetStatisticSummary();} );
        }


        public static IEnumerable<object[]> InvalidDateInputs = new List<object[]>() {
            new object[] { DateTime.UtcNow, null },
            new object[] { null, DateTime.UtcNow },
        };
        private static StatisticSummary CorrectStatisticSummary = new StatisticSummary() {
            TotalBandwidthUsed = 500000000,
            TotalRequestsServed = 20000,
            CacheHitRate = 99.69,
            BandwidthUsedChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5), 100000000 },
                { DateTime.UtcNow.AddDays(-4),  95000000 },
                { DateTime.UtcNow.AddDays(-3),   5000000 },
                { DateTime.UtcNow.AddDays(-2), 100000000 },
                { DateTime.UtcNow.AddDays(-1), 100000000 },
                { DateTime.UtcNow,             100000000 },
            },
            BandwidthCachedChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  99690000 },
                { DateTime.UtcNow.AddDays(-4),  94705500 },
                { DateTime.UtcNow.AddDays(-3),   4984500 },
                { DateTime.UtcNow.AddDays(-2), 99690000 },
                { DateTime.UtcNow.AddDays(-1), 99690000 },
                { DateTime.UtcNow,             99690000 },
            },
            CacheHitRateChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  0 },
                { DateTime.UtcNow.AddDays(-4),  0 },
                { DateTime.UtcNow.AddDays(-3),  0 },
                { DateTime.UtcNow.AddDays(-2),  0 },
                { DateTime.UtcNow.AddDays(-1),  0 },
                { DateTime.UtcNow,              0 },
            },
            RequestsServedChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  4000 },
                { DateTime.UtcNow.AddDays(-4),  1500 },
                { DateTime.UtcNow.AddDays(-3),  2500 },
                { DateTime.UtcNow.AddDays(-2),  8000 },
                { DateTime.UtcNow.AddDays(-1),  3000 },
                { DateTime.UtcNow,              1000 },
            },
            PullRequestsPulledChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  39 },
                { DateTime.UtcNow.AddDays(-4),  540 },
                { DateTime.UtcNow.AddDays(-3),  1 },
                { DateTime.UtcNow.AddDays(-2),  45 },
                { DateTime.UtcNow.AddDays(-1),  110 },
                { DateTime.UtcNow,              69 },
            },
            UserBalanceHistoryChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  10 },
                { DateTime.UtcNow.AddDays(-4),  10 },
                { DateTime.UtcNow.AddDays(-3),  10 },
                { DateTime.UtcNow.AddDays(-2),  10 },
                { DateTime.UtcNow.AddDays(-1),  10 },
                { DateTime.UtcNow,              10 },
            },
            UserStorageUsedChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  10000000 },
                { DateTime.UtcNow.AddDays(-4),  10000000 },
                { DateTime.UtcNow.AddDays(-3),  10000000 },
                { DateTime.UtcNow.AddDays(-2),  10000000 },
                { DateTime.UtcNow.AddDays(-1),  10000000 },
                { DateTime.UtcNow,              10000000 },
            },
            GeoTrafficDistribution = new Dictionary<string, long>() {
                { "EU: London, UK", 14723431 },
                { "NA: Los Angeles, CA", 8861602 },
                { "NA: Atlanta, GA", 182571 },
                { "EU: Paris, FR", 8600 },
                { "NA: New York City, NY", 124442 },
                { "NA: Miami, FL", 16226 },
                { "EU: Amsterdam, NL", 8862 },
                { "NA: San Jose, CA", 7002 },
                { "EU: Madrid, ES", 512 },
                { "NA: Seattle, WA", 2970 },
                { "EU: Prague, CZ", 153750 },
                { "NA: Ashburn, VA", 29658 },
                { "EU: Frankfurt, DE", 30307 },
                { "NA: Dallas, TX", 4521295 }
            },

            Error3xxChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  0 },
                { DateTime.UtcNow.AddDays(-4),  0 },
                { DateTime.UtcNow.AddDays(-3),  0 },
                { DateTime.UtcNow.AddDays(-2),  0 },
                { DateTime.UtcNow.AddDays(-1),  0 },
                { DateTime.UtcNow,              0 },
            },
            Error4xxChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  0 },
                { DateTime.UtcNow.AddDays(-4),  0 },
                { DateTime.UtcNow.AddDays(-3),  0 },
                { DateTime.UtcNow.AddDays(-2),  0 },
                { DateTime.UtcNow.AddDays(-1),  0 },
                { DateTime.UtcNow,              0 },
            },
            Error5xxChart = new Dictionary<DateTime, double>() {
                { DateTime.UtcNow.AddDays(-5),  0 },
                { DateTime.UtcNow.AddDays(-4),  0 },
                { DateTime.UtcNow.AddDays(-3),  0 },
                { DateTime.UtcNow.AddDays(-2),  0 },
                { DateTime.UtcNow.AddDays(-1),  0 },
                { DateTime.UtcNow,              0 },
            }
        };
    }
}