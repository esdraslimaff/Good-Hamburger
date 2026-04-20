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
        [Inject] public NavigationManager Nav { get; set; }
        protected IEnumerable<IGrouping<TipoItem, ItemCardapioDto>> ItensAgrupados => ItensCardapio?.GroupBy(x => x.Tipo) ?? Enumerable.Empty<IGrouping<TipoItem, ItemCardapioDto>>();

        protected List<ItemCardapioDto> ItensCardapio;
        protected List<ItemCardapioDto> Carrinho = new();
        protected string MensagemErro;
        protected bool Processando = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ItensCardapio = await CardapioService.GetItensAsync();
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
                MensagemErro = "Cada pedido pode conter apenas um sanduíche, uma batata e um refrigerante.";
                return;
            }

            if (Carrinho.Any(x => x.Tipo == item.Tipo))
            {
                MensagemErro = $"Você já adicionou um item do tipo {item.Tipo}.";
                return;
            }

            Carrinho.Add(item);
        }

        protected void RemoverDoCarrinho(ItemCardapioDto item)
        {
            Carrinho.Remove(item);
            MensagemErro = string.Empty;
        }

        protected async Task FinalizarPedido()
        {
            try
            {
                Processando = true;
                var request = new PedidoRequest(Carrinho.Select(x => x.Id).ToList());

                var resultado = await PedidoService.CriarPedidoAsync(request);

                if (resultado != null)
                {
                    // Navega para uma tela de sucesso ou detalhes do pedido
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
