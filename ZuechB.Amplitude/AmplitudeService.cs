using Authsome;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZuechB.Amplitude
{
    public interface IAmplitudeService
    {
        Task PostEvents(string apiKey, List<Event> events);
        Task PostEvent(string apiKey, Event @event);
    }

    public class AmplitudeService : IAmplitudeService
    {
        readonly IAuthsomeService authsomeService;
        public AmplitudeService(IAuthsomeService authsomeService)
        {
            this.authsomeService = authsomeService;
            authsomeService.Provider.APIBaseUrl = "https://api.amplitude.com/";
        }

        public async Task PostEvents(string apiKey, List<Event> events)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(events);
            await trackEvents(apiKey, "event", data);
        }

        public async Task PostEvent(string apiKey, Event @event)
        {
            var listEvents = new List<Event>();
            listEvents.Add(@event);
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(listEvents);
            await trackEvents(apiKey, "event", data);
        }

        private async Task trackEvents(string apiKey, string paramName, string paramData)
        {
            var content = new MultipartFormDataContent("----" + DateTime.Now.Ticks);
            var keyContent = new StringContent(apiKey, UTF8Encoding.UTF8, "text/plain");
            content.Add(keyContent, "api_key");

            var data = new StringContent(paramData, UTF8Encoding.UTF8, "application/json");
            content.Add(data, paramName);

            var response = await authsomeService.PostAsync<dynamic>("/httpapi", content);
        }
    }
}