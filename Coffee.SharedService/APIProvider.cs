using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Coffee.APIProvider
{
    public class APIProvider<TModel>:IAPIProvider<TModel>
    {
        protected HttpClient httpService;

        public APIProvider()
        {
        }

        public void Initialize(string apiAddress)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpService= httpClient;
        }
        public async Task<List<TModel>> GetAll()
        {
            var result = await httpService.GetAsync("");
            if (result.IsSuccessStatusCode)
            {
                var storeList = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<TModel>>(storeList);
            }
            return new List<TModel>();
        }

        public async Task<TModel> GetById(string actionName, Guid id)
        {
            var result = await httpService.GetAsync($"{actionName}/{id}");
            if (result.IsSuccessStatusCode)
            {
                var contentResult = result.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<TModel>(contentResult);
                return model;
            }
            throw new Exception();
        }

        public virtual async Task<TModel> Insert(TModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await httpService.PostAsync("", content);
            if (result.IsSuccessStatusCode)
            {
                var contentResult = result.Content.ReadAsStringAsync().Result;
                var resultContent = JsonConvert.DeserializeObject<TModel>(contentResult);
                return resultContent;
            }
            return JsonConvert.DeserializeObject<TModel>(null);
        }

        public bool Update(TModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = httpService.PutAsync("", content);
            return result.IsCompletedSuccessfully;
        }

        public bool Delete(Guid id)
        {
            var result = httpService.DeleteAsync($"{id}");
            return result.IsCompletedSuccessfully;
        }

       
    }
}
