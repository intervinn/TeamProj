using TeamProj.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace TeamProj.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7000/api/"; // Измените на ваш API URL

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T?> GetAsync<T>(string endpoint) where T : class
    {
        var response = await _httpClient.GetAsync(_baseUrl + endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint) where T : class
    {
        var response = await _httpClient.GetAsync(_baseUrl + endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<T>>() ?? new List<T>();
    }

    public async Task<bool> PostAsync<T>(string endpoint, T item) where T : class
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl + endpoint, item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync<T>(string endpoint, T item) where T : class
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new ArgumentException("Item must have an Id property");

        var id = idProperty.GetValue(item)?.ToString();

        var response = await _httpClient.PutAsJsonAsync(_baseUrl + endpoint + "/" + id, item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync<T>(string endpoint, T item) where T : class
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new ArgumentException("Item must have an Id property");

        var id = idProperty.GetValue(item)?.ToString();
        var response = await _httpClient.DeleteAsync(_baseUrl + endpoint + "/" + id);
        return response.IsSuccessStatusCode;
    }
}
s