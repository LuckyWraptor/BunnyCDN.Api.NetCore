using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BunnyCDN.Api
{
    public class JsonWrapper
    {
        public static async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            if (!isJsonContent(httpResponse))
                throw new BunnyInvalidResponseException();

            T deserialized;
            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            try {
                deserialized = JsonConvert.DeserializeObject<T>(jsonString);
            } catch (JsonException) {
                throw new BunnyInvalidResponseException();
            }

            if (deserialized == null)
                throw new BunnyInvalidResponseException();

            return deserialized;
        }

        private static bool isJsonContent(HttpResponseMessage httpResponse)
        {
            if (httpResponse.Content.Headers.ContentType == null)
                return false;

            return (httpResponse.Content.Headers.ContentType.MediaType.ToLower() == "application/json");
        }
    }
}