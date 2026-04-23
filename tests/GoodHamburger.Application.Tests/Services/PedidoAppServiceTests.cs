using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using Moq;

namespace GoodHamburger.Application.Tests.Services
{
    public class PedidoAppServiceTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepoMock;
        private readonly Mock<IItemRepository> _itemRepoMock;
        private readonly Mock<IPromocaoRepository> _promocaoRepoMock;
        private readonly Mock<IValidator<PedidoRequest>> _validatorMock;
        private readonly PedidoAppService _service;

        public PedidoAppServiceTests()
        {
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _itemRepoMock = new Mock<IItemRepository>();
            _promocaoRepoMock = new Mock<IPromocaoRepository>();
            _validatorMock = new Mock<IValidator<PedidoRequest>>();

            _service = new PedidoAppService(_pedidoRepoMock.Object,_itemRepoMock.Object,_promocaoRepoMock.Object,_validatorMock.Object);
        }

        [Fact]
        public async Task CriarPedidoAsync_DeveLancarDomainException_QuandoRequestForInvalido()
        {
            // Arrange
            var request = new PedidoRequest();
            var failures = new List<ValidationFailure> { new ValidationFailure("ItensIds", "Erro de validação") };

            _validatorMock.Setup(v => v.ValidateAsync(request, default))
                          .ReturnsAsync(new ValidationResult(failures));

            // Act
            Func<Task> act = async () => await _service.CriarPedidoAsync(request);

            // Assert
            await act.Should().ThrowAsync<DomainException>().WithMessage("Erro de validação");
            _pedidoRepoMock.Verify(r => r.AddAsync(It.IsAny<Pedido>()), Times.Never);
        }

        [Fact]
        public async Task CriarPedidoAsync_DeveLancarDomainException_QuandoItensNaoForemEncontradosNoBanco()
        {
            var request = new PedidoRequest(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new ValidationResult());

            _itemRepoMock.Setup(r => r.GetItensPorIdsAsync(request.ItensIds))
                         .ReturnsAsync(new List<Item> { new Item("X Burger", 5.0m, TipoItem.Sanduiche) });

            Func<Task> act = async () => await _service.CriarPedidoAsync(request);

            await act.Should().ThrowAsync<DomainException>()
                     .WithMessage("Um ou mais itens selecionados são inválidos.");
        }

        [Fact]
        public async Task CriarPedidoAsync_DeveCriarSalvarERetornarPedidoResponse_QuandoSucesso()
        {
            var itemId = Guid.NewGuid();
            var request = new PedidoRequest(new List<Guid> { itemId });
            var itemRetornado = new Item("X Burger", 5.0m, TipoItem.Sanduiche);

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new ValidationResult());

            _itemRepoMock.Setup(r => r.GetItensPorIdsAsync(request.ItensIds))
                         .ReturnsAsync(new List<Item> { itemRetornado });

            _promocaoRepoMock.Setup(r => r.ObterTodasAtivasAsync())
                             .ReturnsAsync(new List<Promocao>());

            var result = await _service.CriarPedidoAsync(request);

            result.Should().NotBeNull();
            result.TotalFinal.Should().Be(5.0m);
            _pedidoRepoMock.Verify(r => r.AddAsync(It.IsAny<Pedido>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarPedidoAsync_DeveLancarDomainException_QuandoPedidoNaoExistir()
        {
            var id = Guid.NewGuid();
            var request = new PedidoRequest(new List<Guid> { Guid.NewGuid() });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new ValidationResult());
            _pedidoRepoMock.Setup(r => r.GetPedidoComItensAsync(id)).ReturnsAsync((Pedido?)null);

            Func<Task> act = async () => await _service.AtualizarPedidoAsync(id, request);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Pedido não encontrado.");
        }

        [Fact]
        public async Task AtualizarPedidoAsync_DeveAtualizarItensESalvar_QuandoSucesso()
        {
            var pedidoId = Guid.NewGuid();
            var pedidoExistente = new Pedido();

            var novoItemId = Guid.NewGuid();
            var request = new PedidoRequest(new List<Guid> { novoItemId });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new ValidationResult());
            _pedidoRepoMock.Setup(r => r.GetPedidoComItensAsync(pedidoId)).ReturnsAsync(pedidoExistente);

            var itemRetornado = new Item("X Burger", 5.0m, TipoItem.Sanduiche);

            _itemRepoMock.Setup(r => r.GetItensPorIdsAsync(It.IsAny<List<Guid>>()))
                         .ReturnsAsync(new List<Item> { itemRetornado });

            _promocaoRepoMock.Setup(r => r.ObterTodasAtivasAsync()).ReturnsAsync(new List<Promocao>());


            await _service.AtualizarPedidoAsync(pedidoId, request);

            _pedidoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            pedidoExistente.Itens.Should().HaveCount(1);
        }

        [Fact]
        public async Task RemoverAsync_DeveChamarDelete_QuandoPedidoExistir()
        {
            var id = Guid.NewGuid();
            var pedido = new Pedido();
            _pedidoRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(pedido);

            await _service.RemoverAsync(id);

            _pedidoRepoMock.Verify(r => r.DeleteAsync(pedido), Times.Once);
        } //TO-DO: TESTE PARA CASO NÃO ENCONTRAR ID
    }
}
