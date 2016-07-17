using AppBeat.CLI.Types;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppBeat.CLI.API
{
    public class PageSpeed : BaseAPI
    {
        /// <summary>
        /// Example: HTTP POST https://web1.appbeat.io/API/v1/page-speed
        /// JSON payload example (content type must be set to application/json):
        ///     {
        ///          "Secret": "YOUR_APPBEAT_SECRET_KEY",
        ///          "Url": "URL_YOU_WANT_TO_TEST",
        ///          "Location": "TEST_LOCATION"
        ///     }
        /// </summary>
        public async Task<PageSpeedTestRes> SubmitTestAsync(string url, Location location)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Constants.API_TIMEOUT);

                var res = await client.PostAsync($"{Endpoint}/page-speed", new StringContent(JsonConvert.SerializeObject(new PageSpeedSubmitTestReq() {
                    Url = url,
                    Location = Enum.GetName(typeof(Location), location),
                    Secret = Secret
                }), Encoding.UTF8, "application/json"));

                if (res?.Content == null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<PageSpeedTestRes>(await res.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Example: HTTP GET https://web2.appbeat.io/API/v1/page-speed?secret=YOUR_APPBEAT_SECRET_KEY&id=SUBMITTED_TEST_ID
        /// </summary>
        public async Task<PageSpeedTestRes> GetTestAsync(string testId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Constants.API_TIMEOUT);

                var res = await client.GetAsync($"{Endpoint}/page-speed?secret={Secret}&id={testId}");

                if (res?.Content == null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<PageSpeedTestRes>(await res.Content.ReadAsStringAsync());
            }
        }
    }
}
