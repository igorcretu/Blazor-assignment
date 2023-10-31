using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;
    
    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<User> RegisterUser(UserRegistrationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/User", dto);
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        string uri = "/User";
        if (!string.IsNullOrEmpty(username))
        {
            uri += $"?username={username}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);

        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto dto)
    {
        string uri = "/User";
        if (dto.UsernameContains != null)
            uri += $"?usernameContains={dto.UsernameContains}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return users;
    }
}