using GoodHamburger.BlazorWasm.Models;
using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services
{
    public static class CalculadoraPedidoService
    {
        public static PedidoResumo Calcular(List<ItemCardapioDto> itens, List<PromocaoDto> promocoes, decimal descontoPercentualSalvo = 0, bool isEdicao = false)
        {
            var resumo = new PedidoResumo
            {
                Subtotal = itens.Sum(i => i.PrecoUnitario)
            };
            

            if (isEdicao)
            {
                resumo.PercentualDesconto = descontoPercentualSalvo;
                resumo.NomePromocaoAtiva = descontoPercentualSalvo > 0 ? "Desconto do Pedido Original" : "Sem promoção no pedido original";
                return resumo;
            }

            var tiposNoCarrinho = itens.Select(x => x.Tipo).Distinct().ToList();
            var melhorPromocao = promocoes
                .Where(p => p.Requisitos.All(tipoReq => tiposNoCarrinho.Contains(tipoReq)))
                .OrderByDescending(p => p.Percentual)
                .FirstOrDefault();

            if (melhorPromocao != null)
            {
                resumo.PercentualDesconto = melhorPromocao.Percentual;
                resumo.NomePromocaoAtiva = melhorPromocao.Nome;
            }

            return resumo;
        }
    }
}