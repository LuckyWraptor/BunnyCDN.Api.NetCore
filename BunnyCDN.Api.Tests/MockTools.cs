using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace BunnyCDN.Api.Tests
{
    public class MockTools
    {
        public static MockHttpMessageHandler GetNewMockHandler()
        {
            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*/throwunauthorized*").Respond(HttpStatusCode.Unauthorized);
            mockHttp.When("*/throwbadrequest*").Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(BadRequestError));
            mockHttp.When("*/throwinvalidbadrequest*").Respond(HttpStatusCode.BadRequest, "application/json", "");
            mockHttp.When("*/throwinvalidbadrequest1*").Respond(HttpStatusCode.BadRequest, "application/json", "{}");
            mockHttp.When("*/throwinvalidbadrequest2*").Respond(HttpStatusCode.BadRequest, "application/json", "{}{}");
            mockHttp.When("*/throwinvalidokjson*").Respond(HttpStatusCode.OK, "application/json", "{}{}");
            mockHttp.When("*/throwemptyok*").Respond(HttpStatusCode.OK, "application/json", "");
            mockHttp.When("*/thrownotfound*").Respond(HttpStatusCode.NotFound);

            return mockHttp;
        }


        internal static readonly ErrorMessage BadRequestError = new ErrorMessage() { HttpCode = 400, Message = "Specific error message." };
    }
}