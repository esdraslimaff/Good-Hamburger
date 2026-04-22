using GoodHamburger.BlazorWasm.Models;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Components
{
    public partial class CarrinhoResumo
    {
        [Parameter] public List<ItemCardapioDto> ItensNoCarrinho { get; set; } = new();
        [Parameter] public PedidoResumo Resumo { get; set; } = new();
        [Parameter] public bool Processando { get; set; }
        [Parameter] public string MensagemErro { get; set; }
        [Parameter] public EventCallback<ItemCardapioDto> OnRemover { get; set; }
        [Parameter] public EventCallback OnFinalizar { get; set; }
    }
}