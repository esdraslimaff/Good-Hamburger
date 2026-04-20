using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWasm.Services
{
    public class CardapioService : ICardapioService
    {
        private readonly HttpClient _http;
        public CardapioService(HttpClient http) => _http = http;

        public async Task<List<ItemCardapioDto>> GetItensAsync()
            => await _http.GetFromJsonAsync<List<ItemCardapioDto>>("api/Cardapio") ?? new();
    }
}
