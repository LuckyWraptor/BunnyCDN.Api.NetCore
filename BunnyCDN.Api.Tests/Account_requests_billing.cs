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
    public class Account_requests_billing
    {

        [Fact]
        public async void Account_GetBillingSummary_valid()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/billing").Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(CorrectBillingSummary));

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);

            // Act
            BillingSummary summary = await account.GetBillingSummary();

            // Assert
            Assert.Equal(CorrectBillingSummary.Balance, summary.Balance);
            Assert.Equal(CorrectBillingSummary.BillingRecords.Length, summary.BillingRecords.Length);
            Assert.Equal(CorrectBillingSummary.BillingRecords[0].Timestamp, summary.BillingRecords[0].Timestamp);
        }

        [Fact]
        public async void Account_GetBillingSummary_unauthorized()
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/billing").Respond(HttpStatusCode.Unauthorized);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);
            
            // Act & Arrange
            await Assert.ThrowsAsync<BunnyUnauthorizedException>( async() => { await account.GetBillingSummary(); });
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("{}{}")]
        [InlineData("[]")]
        public async void Account_GetBillingSummary_invalid(string contentString)
        {
            // Arrange
            MockHttpMessageHandler mockHttp = MockTools.GetNewMockHandler();
            mockHttp.When(HttpMethod.Get, "*/billing").Respond(HttpStatusCode.OK, "application/json", contentString);

            AccountKey accKey = new AccountKey();
            accKey.SetToken("17989543-2154-6867-3566-71474693165007735103-0594-4591-2132-259238857481", mockHttp);

            Account account = new Account(accKey);
            
            // Act & Arrange
            await Assert.ThrowsAsync<BunnyInvalidResponseException>( async() => { await account.GetBillingSummary(); });
        }

        private static BillingSummary CorrectBillingSummary = new BillingSummary() {
            Balance = 100,
            ThisMonthCharges = 5.0000420,
            BillingRecords = new BillingRecord[] {
                new BillingRecord() {
                    Id = 31247,
                    Amount = 25,
                    Payer = "billing@mycompany.com",
                    PaymentId = "4xq2kad",
                    Timestamp = DateTime.UtcNow,
                    Type = BillingType.CreditCard
                },
                new BillingRecord() {
                    Id = 21247,
                    Amount = 12.23,
                    Payer = "billing@mycompany.com",
                    PaymentId = "4xq2kab",
                    Timestamp = DateTime.UtcNow,
                    Type = BillingType.Bitcoin
                }
            },
            MonthlyChargesStorage = 1.500001,
            MonthlyChargesEUTraffic = 1.499999,
            MonthlyChargesUSTraffic = 0.5000420,
            MonthlyChargesASIATraffic = 1.4,
            MonthlyChargesSATraffic = 0.099958
        };
    }
}