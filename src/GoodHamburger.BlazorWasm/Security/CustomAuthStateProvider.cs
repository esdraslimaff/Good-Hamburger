using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace GoodHamburger.BlazorWasm.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSInProcessRuntime _jsRuntime;

        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = (IJSInProcessRuntime)jsRuntime;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = _jsRuntime.Invoke<string>("localStorage.getItem", "authToken");

                if (string.IsNullOrWhiteSpace(token) || token == "undefined" || token == "null")
                {
                    return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }

                var claims = ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt",
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
                    ClaimTypes.Role);

                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            }
            catch (Exception)
            {
                _jsRuntime.InvokeVoid("localStorage.removeItem", "authToken");
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }

        public void MarcarUsuarioComoLogado(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
                ClaimTypes.Role);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public void MarcarUsuarioComoDeslogado()
        {
            _jsRuntime.InvokeVoid("localStorage.removeItem", "authToken");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        //TO-DO: CONVERTER CLAIM STRING PARA ENUM
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp =>
            {
                var value = kvp.Value.ToString();
                if (kvp.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" || kvp.Key == "role")
                {
                    value = value == "1" ? "Admin" : (value == "2" ? "Atendente" : value);
                    return new Claim(ClaimTypes.Role, value);
                }

                if (kvp.Key == "email")
                {
                    return new Claim(ClaimTypes.Email, value);
                }

                return new Claim(kvp.Key, value);
            });
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}