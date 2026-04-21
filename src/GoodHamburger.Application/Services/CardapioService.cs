using GoodHamburger.Application.Interfaces;
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
    public class CardapioService : ICardapioService
    {
        private readonly IItemRepository _itemRepository;

        public CardapioService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ItemCardapioDto>> ObterItensAsync()
        {
            var itens = await _itemRepository.GetAllAsync();
            return itens.Adapt<IEnumerable<ItemCardapioDto>>();
        }
    }
}
