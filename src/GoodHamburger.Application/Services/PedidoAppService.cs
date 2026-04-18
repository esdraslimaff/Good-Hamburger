using FluentValidation;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
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
        private readonly IValidator<PedidoRequest> _validator;
        public PedidoAppService(IPedidoRepository pedidoRepository, IItemRepository itemRepository, IValidator<PedidoRequest> validator)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
            _validator = validator;
        }

        public async Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var firstError = validationResult.Errors.First().ErrorMessage;
                throw new DomainException(firstError);
            }

            var itensNoBanco = await _itemRepository.GetItensPorIdsAsync(request.ItensIds);

            if (itensNoBanco.Count() != request.ItensIds.Distinct().Count())
            {
                throw new DomainException("Um ou mais itens selecionados são inválidos ou não existem no cardápio.");
            }

            var novoPedido = new Pedido();

            foreach (var item in itensNoBanco)
            {
                novoPedido.AdicionarItem(item);
            }

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
    }
}
