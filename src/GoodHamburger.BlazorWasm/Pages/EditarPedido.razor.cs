using GoodHamburger.BlazorWasm.Models;
using GoodHamburger.BlazorWasm.Services;
using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class EditarPedido
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] public ICardapioService CardapioService { get; set; }
        [Inject] public IPedidoService PedidoService { get; set; }
        [Inject] public IPromocaoService PromocaoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected List<ItemCardapioDto> ItensCardapio = new();
        protected List<PromocaoDto> Promocoes = new();
        protected List<ItemCardapioDto> ItensNoCarrinho = new();

        protected PedidoResumo Resumo = new();

        protected bool Carregando { get; set; } = true;
        protected bool Processando { get; set; }
        protected string MensagemErro { get; set; }

        protected IEnumerable<IGrouping<TipoItem, ItemCardapioDto>> ItensAgrupados =>
            ItensCardapio?.GroupBy(x => x.Tipo) ?? Enumerable.Empty<IGrouping<TipoItem, ItemCardapioDto>>();

        private decimal descontoPercentualRegistrado;
        private Guid? promocaoIdRegistrada;
        private bool pedidoFoiAlterado = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Carregando = true;

                var taskCardapio = CardapioService.GetItensAsync();
                var taskPedido = PedidoService.GetPorIdAsync(Id);

                await Task.WhenAll(taskCardapio, taskPedido);

                ItensCardapio = await taskCardapio ?? new();
                var pedidoAtual = await taskPedido;

                descontoPercentualRegistrado = pedidoAtual?.DescontoPercentual ?? 0;
                promocaoIdRegistrada = pedidoAtual?.PromocaoId;

                if (pedidoAtual?.Itens != null)
                {
                    ItensNoCarrinho = pedidoAtual.Itens.ToList();
                }

                if (promocaoIdRegistrada.HasValue)
                {
                    var promocaoOriginal = await PromocaoService.GetPromocaoPorIdAsync(promocaoIdRegistrada.Value);

                    Promocoes = promocaoOriginal != null ? new List<PromocaoDto> { promocaoOriginal } : new List<PromocaoDto>();
                }
                else
                {
                    Promocoes = new List<PromocaoDto>();
                }

                AtualizarEstado();
            }
            catch (Exception ex)
            {
                MensagemErro = "Erro ao carregar dados do pedido: " + ex.Message;
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
                MensagemErro = "Cada pedido pode conter no máximo 3 itens.";
                return;
            }

            if (ItensNoCarrinho.Any(x => x.Tipo == item.Tipo))
            {
                MensagemErro = $"Você já adicionou um item do tipo {item.Tipo}.";
                return;
            }
            ItensNoCarrinho.Add(item);
            pedidoFoiAlterado = true;
            AtualizarEstado();
        }

        protected void RemoverDoCarrinho(ItemCardapioDto item)
        {
            var itemNaLista = ItensNoCarrinho.FirstOrDefault(x => x.Id == item.Id);
            if (itemNaLista != null)
            {
                ItensNoCarrinho.Remove(itemNaLista);
                pedidoFoiAlterado = true;
            }

            MensagemErro = string.Empty;
            AtualizarEstado();
        }

        private void AtualizarEstado()
        {
            var promocoesPermitidas = promocaoIdRegistrada.HasValue ? Promocoes.Where(p => p.Id == promocaoIdRegistrada.Value).ToList() : new List<PromocaoDto>();

            Resumo = CalculadoraPedidoService.Calcular(ItensNoCarrinho, promocoesPermitidas, descontoPercentualRegistrado, pedidoFoiAlterado);
            StateHasChanged();
        }

        protected async Task SalvarAlteracoes()
        {
            if (!ItensNoCarrinho.Any())
            {
                MensagemErro = "Sua bandeja não pode estar vazia!";
                return;
            }

            Processando = true;
            MensagemErro = string.Empty;

            try
            {
                var request = new PedidoRequest(ItensNoCarrinho.Select(x => x.Id).ToList());
                await PedidoService.AtualizarAsync(Id, request);
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