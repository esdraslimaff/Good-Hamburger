using GoodHamburger.Domain.Enums;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class EditarPedido
    {
        [Parameter] public Guid Id { get; set; }
        protected PedidoRequest Request { get; set; } = new() { ItensIds = new List<Guid>() };
        protected List<ItemCardapioDto> ItensNoCarrinho { get; set; } = new();
        protected List<ItemCardapioDto> ItensCardapio { get; set; } = new();
        protected IEnumerable<IGrouping<TipoItem, ItemCardapioDto>> ItensAgrupados => ItensCardapio?.GroupBy(x => x.Tipo) ?? Enumerable.Empty<IGrouping<TipoItem, ItemCardapioDto>>();
        protected bool Carregando { get; set; } = true;
        protected bool Processando { get; set; }
        protected string MensagemErro { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Carregando = true;

                var itensCardapio = await CardapioService.GetItensAsync();
                var pedidoAtual = await PedidoService.GetPorIdAsync(Id);

                if (itensCardapio != null)
                    ItensCardapio = itensCardapio;

                if (pedidoAtual?.Itens != null)
                {
                    ItensNoCarrinho = pedidoAtual.Itens.ToList();
                    AtualizarIdsDoRequest();
                }
            }
            catch (Exception ex)
            {
                MensagemErro = "Erro ao carregar itens: " + ex.Message;
            }
            finally
            {
                Carregando = false;
            }
        }
        protected void AdicionarAoCarrinho(ItemCardapioDto item)
        {
            MensagemErro = string.Empty;
            if (ItensNoCarrinho.Count >= 3)
            {
                MensagemErro = "Cada pedido pode conter apenas um sanduíche, uma batata e um refrigerante.";
                return;
            }

            if (ItensNoCarrinho.Any(x => x.Tipo == item.Tipo))
            {
                MensagemErro = $"Você já adicionou um item do tipo {item.Tipo}.";
                return;
            }
            ItensNoCarrinho.Add(item);
            AtualizarIdsDoRequest();
            StateHasChanged();
        }

        protected void RemoverDoCarrinho(ItemCardapioDto item)
        {

            var itemNaLista = ItensNoCarrinho.FirstOrDefault(x => x.Id == item.Id);
            if (itemNaLista != null)
            {
                ItensNoCarrinho.Remove(itemNaLista);
            }

            AtualizarIdsDoRequest();
            StateHasChanged();
        }

        private void AtualizarIdsDoRequest()
        {
            Request.ItensIds = ItensNoCarrinho.Select(x => x.Id).ToList();
        }

        protected async Task SalvarAlteracoes()
        {
            if (!Request.ItensIds.Any())
            {
                MensagemErro = "Sua bandeja não pode estar vazia para atualizar o pedido!";
                return;
            }

            Processando = true;
            MensagemErro = string.Empty;

            try
            {
                await PedidoService.AtualizarAsync(Id, Request);
                Nav.NavigateTo("/pedidos");
            }
            catch (Exception ex)
            {
                MensagemErro = "Erro ao atualizar: " + ex.Message;
            }
            finally
            {
                Processando = false;
            }
        }

        protected void Voltar() => Nav.NavigateTo("/pedidos");
    }
}