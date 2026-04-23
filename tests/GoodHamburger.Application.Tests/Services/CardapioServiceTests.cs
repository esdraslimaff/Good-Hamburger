using FluentAssertions;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Tests.Services
{
    public class CardapioServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepoMock;
        private readonly CardapioService _service;

        public CardapioServiceTests()
        {
            _itemRepoMock = new Mock<IItemRepository>();
            _service = new CardapioService(_itemRepoMock.Object);
        }

        [Fact]
        public async Task ObterItensAsync_DeveRetornarListaDeItemCardapioDto()
        {
            var itensMock = new List<Item>
            {
                new Item("X Burger", 5.0m, TipoItem.Sanduiche),
                new Item("Batata frita", 2.0m, TipoItem.Acompanhamento)
            };

            _itemRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(itensMock);

            var result = await _service.ObterItensAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Nome.Should().Be("X Burger");
        }
    }
}
