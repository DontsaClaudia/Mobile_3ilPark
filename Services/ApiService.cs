using Mobile_3ilPark.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
            BaseAddress = new Uri("http://10.0.2.2:5165")
        };
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var loginUrl = "login";

        var loginData = new
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync(loginUrl, loginData);
        Console.WriteLine();

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            result.IsSuccess = true;
            return result;
        }

        return new LoginResult { IsSuccess = false };
    }

    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string? accessToken { get; set; }
    }
    public async Task<List<Computers>> GetComputersAsync(string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var computers = await _httpClient.GetFromJsonAsync<List<Computers>>("api/Computers");
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

    public async Task<Users> GetUserAsync(int Id)
    {
        var apiUrl = $"api/Users/{Id}"; // Remplacez par l'URL de votre API
        var response = await _httpClient.GetFromJsonAsync<Users>(apiUrl);
        return response;
    }
}