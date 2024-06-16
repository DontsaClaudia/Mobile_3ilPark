using Microsoft.Maui.Controls.PlatformConfiguration;
using Mobile_3ilPark.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Mobile_3ilPark.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5010")
        };
    }

    /// <summary>
    /// Authorisattion pour le token
    /// </summary>
    private void AddAuthorizationHeader()
    {
        var token = Preferences.Get("AuthToken", string.Empty);
        Console.WriteLine($"Using token: {token}");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    /// <summary>
    /// Fonction pour la connexion à l'application via l'Api et récuparation du token
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var loginUrl = "login";

        var loginData = new
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync(loginUrl, loginData);
       

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
        public int UserId { get; set; }
    }

    /// <summary>
    ///  Fonction pour afficher les données de 'utilisateur connecté
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<Users> GetUserAsync(int userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var UserApi = $"api/Users/{userId}";
        var response = await _httpClient.GetFromJsonAsync<Users>(UserApi);
        return response;
    }

    /// <summary>
    ///  fonction pour lister les computers enregistrés
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
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
            
            Console.WriteLine($"Request error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        return new List<Computers>();
    }

    /// <summary>
    ///  Recuperer un computer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Computers> GetComputerByIdAsync(int id)
    {
        AddAuthorizationHeader();
        var apiUrl = $"api/Computers/{id}"; 
        var response = await _httpClient.GetFromJsonAsync<Computers>(apiUrl);
        return response;
    }

    /// <summary>
    /// Fonction  pour modifier un computer
    /// </summary>
    /// <param name="computer"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task UpdateComputerAsync(Computers computer)
    {
        AddAuthorizationHeader();
        var apiUrl = $"api/Computers/{computer.Id}"; 
        await _httpClient.PutAsJsonAsync(apiUrl, computer);
    }

    /// <summary>
    /// Fonction pour supprimer un computer
    /// </summary>
    /// <param name="id"></param>

    /// <returns></returns>
    public async Task DeleteComputerAsync(int id)
    {
        AddAuthorizationHeader();
        var apiUrl = $"api/Computers/{id}"; 
        await _httpClient.DeleteAsync(apiUrl);
    }


    public async Task<List<Rooms>> GetRoomsAsync()
    {
        AddAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<List<Rooms>>("api/Rooms");
    }

    public async Task<Rooms> GetRoomByIdAsync(int id)
    {
        AddAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<Rooms>($"api/Rooms/{id}");
    }

    public async Task CreateRoomAsync(Rooms room)
    {
        AddAuthorizationHeader();
        await _httpClient.PostAsJsonAsync("api/Rooms", room);
    }

    public async Task UpdateRoomAsync(Rooms room)
    {
        AddAuthorizationHeader();
        await _httpClient.PutAsJsonAsync($"api/Rooms/{room.Id}", room);
    }

    public async Task DeleteRoomAsync(int id)
    {
        AddAuthorizationHeader();   
        await _httpClient.DeleteAsync($"api/Rooms/{id}");
    }
}