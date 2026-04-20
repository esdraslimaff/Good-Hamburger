using GoodHamburger.Domain.Enums;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Components
{
    public partial class CarrinhoResumo
    {
        [Parameter] public List<ItemCardapioDto> ItensNoCarrinho { get; set; } = new();
        [Parameter] public bool Processando { get; set; }
        [Parameter] public string MensagemErro { get; set; }
        [Parameter] public EventCallback<ItemCardapioDto> OnRemover { get; set; }
        [Parameter] public EventCallback OnFinalizar { get; set; }

        private decimal Subtotal => ItensNoCarrinho.Sum(x => x.PrecoUnitario);
        private decimal PercentualDesconto { get; set; }
        private decimal ValorDesconto => Subtotal * PercentualDesconto;
        private decimal TotalFinal => Subtotal - ValorDesconto;

        protected override void OnParametersSet()
        {
            CalcularDesconto();
        }

        private void CalcularDesconto()
        {
            var tipos = ItensNoCarrinho.Select(x => x.Tipo).ToList();
            PercentualDesconto = 0;

            if (tipos.Contains(TipoItem.Sanduiche) && tipos.Contains(TipoItem.Bebida) && tipos.Contains(TipoItem.Acompanhamento))
                PercentualDesconto = 0.20m;
            else if (tipos.Contains(TipoItem.Sanduiche) && tipos.Contains(TipoItem.Bebida))
                PercentualDesconto = 0.15m;
            else if (tipos.Contains(TipoItem.Sanduiche) && tipos.Contains(TipoItem.Acompanhamento))
                PercentualDesconto = 0.10m;
        }
    }
}
