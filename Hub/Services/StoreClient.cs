using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Hub.Services
{
    public class StoreClient : IStoreClient
    {
        private readonly IOptionsSnapshot<HubOptions> _options;
        private readonly HttpClient _httpClient;

        public StoreClient(IOptionsSnapshot<HubOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task BulkAdd(IEnumerable<ProcessedAgentData> datas)
        {
            var request = new {Models = datas};
            await _httpClient.PostAsJsonAsync(_options.Value.StoreBulkAddUrl, request);
        }
    }
}