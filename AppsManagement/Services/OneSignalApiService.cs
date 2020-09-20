using AppsManagement.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppsManagement.Services
{
    public class OneSignalApiService : IOneSignalApiService
    {
        private readonly HttpClient httpClient;

        public OneSignalApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<AppModel>> GetAllAsync()
        {
            var response = await httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var appList = JsonConvert.DeserializeObject<IEnumerable<AppModel>>(content);
                return appList;
            }
            return null;
        }

        public async Task<AppModel> GetAsync(string id)
        {
            var response = await httpClient.GetAsync(id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var app = JsonConvert.DeserializeObject<AppModel>(content);
                return app;
            }
            return null;
        }

        public async Task<AppModel> CreateApp(AppModel appModel)
        {
            var myContent = JsonConvert.SerializeObject(appModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("", byteContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addedApp = JsonConvert.DeserializeObject<AppModel>(content);
                return addedApp;
            }
            return null;
        }

        public async Task<AppModel> UpdateApp(AppModel appModel)
        {
            var myContent = JsonConvert.SerializeObject(appModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PutAsync(appModel.AppId, byteContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addedApp = JsonConvert.DeserializeObject<AppModel>(content);
                return addedApp;
            }
            return null;
        }
    }
}
