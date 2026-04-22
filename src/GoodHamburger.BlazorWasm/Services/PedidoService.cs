using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWasm.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        public PedidoService(HttpClient http) => _http = http;

        public async Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/Pedidos", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PedidoResponse>();
        }

        public async Task<List<PedidoResponse>> GetTodosAsync()
            => await _http.GetFromJsonAsync<List<PedidoResponse>>("api/Pedidos") ?? new();

        public async Task<PedidoResponse> GetPorIdAsync(Guid id)
            => await _http.GetFromJsonAsync<PedidoResponse>($"api/Pedidos/{id}");

        public async Task DeletarAsync(Guid id)
            => await _http.DeleteAsync($"api/Pedidos/{id}");

        public async Task AtualizarAsync(Guid id, PedidoRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/pedidos/{id}", request);
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception(erro);
            }
        }
    }
}
