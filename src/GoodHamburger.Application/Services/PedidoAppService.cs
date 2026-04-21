using FluentValidation;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPromocaoRepository _promocao;
        private readonly IValidator<PedidoRequest> _validator;
        public PedidoAppService(IPedidoRepository pedidoRepository, IItemRepository itemRepository, IPromocaoRepository Promocao , IValidator<PedidoRequest> validator)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
            _promocao = Promocao;
            _validator = validator;
        }

        public async Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new DomainException(validationResult.Errors.First().ErrorMessage);

            var itensNoBanco = await _itemRepository.GetItensPorIdsAsync(request.ItensIds);
            if (itensNoBanco.Count() != request.ItensIds.Distinct().Count())
                throw new DomainException("Um ou mais itens selecionados são inválidos.");

            var novoPedido = new Pedido();
            foreach (var item in itensNoBanco)
            {
                novoPedido.AdicionarProduto(item);
            }

            var regrasAtivas = await _promocao.ObterTodasAtivasAsync();

            novoPedido.ProcessarPedido(regrasAtivas);

            await _pedidoRepository.AddAsync(novoPedido);

            return novoPedido.Adapt<PedidoResponse>();
        }

        public async Task<IEnumerable<PedidoResponse>> ListarTodosAsync()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            return pedidos.Adapt<IEnumerable<PedidoResponse>>();
        }

        public async Task<PedidoResponse?> ObterPorIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoComItensAsync(id);
            return pedido?.Adapt<PedidoResponse>();
        }

        public async Task RemoverAsync(Guid id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido != null)
            {
                await _pedidoRepository.DeleteAsync(pedido);
            }
        }
        public async Task AtualizarPedidoAsync(Guid id, PedidoRequest request)
        {
            var pedido = await _pedidoRepository.GetPedidoComItensAsync(id);
            if (pedido == null) throw new DomainException("Pedido não encontrado.");

            var idsDesejados = request.ItensIds.ToList();

            var idsAtuais = pedido.Itens.Select(i => i.Id).ToList();

            var idsParaRemover = idsAtuais.Except(idsDesejados).ToList();

            var idsParaAdicionar = idsDesejados.Except(idsAtuais).ToList();

            foreach (var idRemover in idsParaRemover)
            {
                pedido.RemoverProduto(idRemover);
            }

            if (idsParaAdicionar.Any())
            {
                var produtosNovos = await _itemRepository.GetItensPorIdsAsync(idsParaAdicionar);
                foreach (var produto in produtosNovos)
                {
                    pedido.AdicionarProduto(produto);
                }
            }

            var regrasAtivas = await _promocao.ObterTodasAtivasAsync();
            pedido.ProcessarPedido(regrasAtivas);

            await _pedidoRepository.SaveChangesAsync();
        }
    }
}
