using eintech.domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eintech.web.Services
{
    public interface IPersonService
    {
        Task Delete(Guid id);

        Task<List<Person>> Get();

        Task<Person> GetById(Guid id);

        Task<Person> Create(Person person);
    }

    public class PersonService : IPersonService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient , IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<List<Person>> Get()
        {
            var url = $"{_settings.Value.WebAPI}";

            var response = await _httpClient.GetStringAsync(url);

            var model = JsonConvert.DeserializeObject<List<Person>>(response);

            return model;
        }

        public async Task<Person> GetById(Guid id)
        {
            var url = $"{_settings.Value.WebAPI}/{id}";

            var response = await _httpClient.GetStringAsync(url);

            var model = JsonConvert.DeserializeObject<Person>(response);

            return model;
        }

        public async Task<Person> Create(Person person)
        {
            var url = $"{_settings.Value.WebAPI}";

            var jsonPerson = new StringContent(JsonConvert.SerializeObject(person), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonPerson);

            response.EnsureSuccessStatusCode();

            var model = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());

            return model;

        }

        public async Task Delete(Guid id)
        {
            var url = $"{_settings.Value.WebAPI}/{id}";

            var response = await _httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}
