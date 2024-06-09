using Mobile_3ilPark.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mobile_3ilPark.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7227") 
        };
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var loginUrl = "http://10.0.2.2:7227/api/Users";

        var loginData = new
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync(loginUrl, loginData);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            return result;
        }

        return new LoginResult { IsSuccess = false };
    }

    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
    }
    public async Task<List<Computers>> GetComputersAsync()
    {
        try
        {
            var computers = await _httpClient.GetFromJsonAsync<List<Computers>>("http://10.0.2.2:7227/api/Computers");
            return computers ?? new List<Computers>();
        }
        catch (HttpRequestException httpEx)
        {
            // Gérer les erreurs de requête HTTP
            Console.WriteLine($"Request error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            // Gérer les autres erreurs
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        return new List<Computers>();
    }
}