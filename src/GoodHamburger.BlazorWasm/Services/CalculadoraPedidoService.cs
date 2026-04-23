using GoodHamburger.BlazorWasm.Models;
using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services
{
    public static class CalculadoraPedidoService
    {
        public static PedidoResumo Calcular(List<ItemCardapioDto> itens, List<PromocaoDto> promocoesPermitidas, decimal descontoPercentualSalvo = 0, bool foiAlterado = false)
        {
            var resumo = new PedidoResumo
            {
                Subtotal = itens.Sum(i => i.PrecoUnitario)
            };

            if (!foiAlterado && descontoPercentualSalvo > 0)
            {
                var promoOriginal = promocoesPermitidas.FirstOrDefault();
                resumo.PercentualDesconto = descontoPercentualSalvo;
                resumo.NomePromocaoAtiva = promoOriginal?.Nome ?? "Promoção Original";
                return resumo;
            }

            var tiposNoCarrinho = itens.Select(x => x.Tipo).Distinct().ToList();

            var melhorPromocao = promocoesPermitidas
                .Where(p => p.Requisitos.All(tipoReq => tiposNoCarrinho.Contains(tipoReq)) && p.Requisitos.Count == tiposNoCarrinho.Count)
                .OrderByDescending(p => p.Percentual)
                .FirstOrDefault();

            if (melhorPromocao != null)
            {
                resumo.PercentualDesconto = melhorPromocao.Percentual;
                resumo.NomePromocaoAtiva = melhorPromocao.Nome;
            }
            else
            {
                resumo.PercentualDesconto = 0;
                resumo.NomePromocaoAtiva = "Sem promoção aplicável";
            }

            return resumo;
        }
    }
}