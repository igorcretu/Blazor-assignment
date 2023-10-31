using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWASM.Auth;
using BlazorWASM.Services;
using BlazorWASM.Services.Http;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7022") 
        }
);
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IPostService, PostHttpClient>();
builder.Services.AddScoped<ICommentService, CommentHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

AuthorizationPolicies.AddPolicies(builder.Services);
await builder.Build().RunAsync();