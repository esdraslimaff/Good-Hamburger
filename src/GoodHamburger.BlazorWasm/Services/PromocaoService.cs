using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWasm.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly HttpClient _http;
        public PromocaoService(HttpClient http) => _http = http;

        public async Task<List<PromocaoDto>> GetPromocoesAtivasAsync()
            => await _http.GetFromJsonAsync<List<PromocaoDto>>("api/Promocao/PromocoesAtivas") ?? new();

        public async Task<List<PromocaoDto>> GetPromocoesAsync()
            => await _http.GetFromJsonAsync<List<PromocaoDto>>("api/Promocao") ?? new();

        public async Task<PromocaoDto?> GetPromocaoPorIdAsync(Guid id)
            => await _http.GetFromJsonAsync<PromocaoDto>($"api/Promocao/{id}");

        public async Task AlternarStatusAsync(Guid id)
        {
            var response = await _http.PatchAsync($"api/Promocao/{id}/alternar-status", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao alterar o status da promoção.");
            }
        }
    }
}
