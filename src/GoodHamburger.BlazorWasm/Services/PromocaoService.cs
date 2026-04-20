using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWasm.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly HttpClient _http;
        public PromocaoService(HttpClient http) => _http = http;

        public async Task<List<PromocaoDto>> GetPromocoesAsync()
            => await _http.GetFromJsonAsync<List<PromocaoDto>>("api/Promocao") ?? new();
    }
}
