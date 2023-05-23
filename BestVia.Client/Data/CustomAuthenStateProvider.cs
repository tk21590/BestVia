using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BestVia.Client.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService { get; set; }
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string jwt_token = await _sessionStorageService.GetItemAsync<string>("jwt_token");
            if (string.IsNullOrEmpty(jwt_token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseJWT(jwt_token), "jwt")));

        }

        private IEnumerable<Claim> ParseJWT(string jwt_token)
        {
            var claims = new List<Claim>();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt_token);
            var tokenS = jsonToken as JwtSecurityToken;
            claims = tokenS.Claims.ToList();

            return claims;


        }
        public async Task MarkUserAsAuthenticatedAsync()
        {

            var authen = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authen));

        }
        public async Task MarkUserAsLogOutAsync()
        {
            await _sessionStorageService.RemoveItemAsync("jwt_token");
            var authen = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authen));

        }
    }
}
