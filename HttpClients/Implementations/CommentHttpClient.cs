using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient client;
    
    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Comment> CreateCommentAsync(CommentCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Comment", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Comment comment = JsonSerializer.Deserialize<Comment>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return comment;
    }

    public async Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto dto)
    {
        string uri = "/Comment";
        if (dto.PostId != null)
        {
            uri += $"?postId={dto.PostId}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Comment> comments = JsonSerializer.Deserialize<IEnumerable<Comment>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return comments;
    }
}