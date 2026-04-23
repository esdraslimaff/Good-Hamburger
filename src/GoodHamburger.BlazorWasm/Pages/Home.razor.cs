using GoodHamburger.BlazorWasm.Models;
using GoodHamburger.BlazorWasm.Services;
using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject] public ICardapioService CardapioService { get; set; }
        [Inject] public IPedidoService PedidoService { get; set; }
        [Inject] public IPromocaoService PromocaoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected List<ItemCardapioDto> ItensCardapio;
        protected List<PromocaoDto> Promocoes = new();
        protected List<ItemCardapioDto> Carrinho = new();
        protected PedidoResumo Resumo = new();
        protected string MensagemErro;
        protected bool Processando = false;

        protected IEnumerable<IGrouping<TipoItem, ItemCardapioDto>> ItensAgrupados =>
            ItensCardapio?.GroupBy(x => x.Tipo) ?? Enumerable.Empty<IGrouping<TipoItem, ItemCardapioDto>>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var taskCardapio = CardapioService.GetItensAsync();
                var taskPromocoes = PromocaoService.GetPromocoesAtivasAsync();

                await Task.WhenAll(taskCardapio, taskPromocoes);

                ItensCardapio = await taskCardapio ?? new();
                Promocoes = await taskPromocoes ?? new();

                AtualizarEstado();
            }
            catch (Exception)
            {
                MensagemErro = "Erro ao carregar o cardápio. Verifique se a API está rodando.";
            }
        }

        protected void AdicionarAoCarrinho(ItemCardapioDto item)
        {
            MensagemErro = string.Empty;

            if (Carrinho.Count >= 3)
            {
                MensagemErro = "Cada pedido pode conter no máximo 3 itens.";
                return;
            }

            if (Carrinho.Any(x => x.Tipo == item.Tipo))
            {
                MensagemErro = $"Você já adicionou um item do tipo {item.Tipo}.";
                return;
            }

            Carrinho.Add(item);
            AtualizarEstado();
        }

        protected void RemoverDoCarrinho(ItemCardapioDto item)
        {
            Carrinho.Remove(item);
            MensagemErro = string.Empty;
            AtualizarEstado();
        }

        private void AtualizarEstado()
        {
            Resumo = CalculadoraPedidoService.Calcular(Carrinho, Promocoes);
            StateHasChanged();
        }

        protected async Task FinalizarPedido()
        {
            if (!Carrinho.Any())
            {
                MensagemErro = "Sua bandeja está vazia!";
                return;
            }

            try
            {
                Processando = true;
                var request = new PedidoRequest(Carrinho.Select(x => x.Id).ToList());
                var resultado = await PedidoService.CriarPedidoAsync(request);

                if (resultado != null)
                {
                    Nav.NavigateTo($"/pedidos/{resultado.Id}");
                }
            }
            catch (Exception ex)
            {
                MensagemErro = "Erro ao processar pedido: " + ex.Message;
            }
            finally
            {
                Processando = false;
            }
        }
    }
}