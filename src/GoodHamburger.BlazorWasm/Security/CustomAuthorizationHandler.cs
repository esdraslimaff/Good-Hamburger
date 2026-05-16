using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace GoodHamburger.BlazorWasm.Security
{
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        private readonly IJSInProcessRuntime _jsRuntime;

        public CustomAuthorizationHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = (IJSInProcessRuntime)jsRuntime;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _jsRuntime.Invoke<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Retorna a base do HTTP imediatamente
            return base.SendAsync(request, cancellationToken);
        }
    }
}
