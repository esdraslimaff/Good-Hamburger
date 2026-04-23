using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Tests.Entities
{
    public class PedidoTests
    {

        [Fact]
        public void Construtor_DeveInicializarListaDeItensComoVazia()
        {
            var pedido = new Pedido();

            pedido.Itens.Should().NotBeNull();
            pedido.Itens.Should().BeEmpty();
        }

        [Fact]
        public void Construtor_DeveInicializarValoresFinanceirosZerados()
        {
            var pedido = new Pedido();

            pedido.Subtotal.Should().Be(0m);
            pedido.DescontoPercentual.Should().Be(0m);
            pedido.ValorDesconto.Should().Be(0m);
            pedido.TotalFinal.Should().Be(0m);
        }

        [Fact]
        public void Construtor_DeveInicializarSemPromocaoVinculada()
        {
            var pedido = new Pedido();

            pedido.PromocaoId.Should().BeNull();
        }

        [Fact]
        public void Construtor_DeveInicializarIdEDataDeCriacao()
        {
            var pedido = new Pedido();

            pedido.Id.Should().NotBeEmpty("o pedido deve ter um identificador único desde a criação");
            pedido.DataCriacao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
        [Fact]
        public void AdicionarProduto_DeveAdicionarItemERecalcularTotais()
        {
            var pedido = new Pedido();
            var item = new Item("X Burger", 5.00m, TipoItem.Sanduiche);

            pedido.AdicionarProduto(item);

            pedido.Itens.Should().HaveCount(1);
            pedido.Subtotal.Should().Be(5.00m);
            pedido.TotalFinal.Should().Be(5.00m);
            pedido.ValorDesconto.Should().Be(0);
        }

        [Fact]
        public void AdicionarProduto_DeveLancarExcecao_QuandoAdicionarItemDoMesmoTipo()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            var outroSanduiche = new Item("X Bacon", 7.00m, TipoItem.Sanduiche);

            Action action = () => pedido.AdicionarProduto(outroSanduiche);

            action.Should().Throw<DomainException>()
                  .WithMessage("O pedido já contém um item do tipo Sanduiche.");
        }

        [Fact]
        public void AdicionarProduto_DeveLancarExcecao_QuandoPassarDeTresItens()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 2.00m, TipoItem.Acompanhamento));
            pedido.AdicionarProduto(new Item("Refrigerante", 2.50m, TipoItem.Bebida));

            var itemExtra = new Item("Extra", 10.0m, (TipoItem)99);

            Action action = () => pedido.AdicionarProduto(itemExtra);

            action.Should().Throw<DomainException>()
                  .WithMessage("O pedido já atingiu o limite máximo de 3 itens.");
        }

        [Fact]
        public void AdicionarProduto_QuandoPedidoJaTiverDesconto_DeveZerarDescontoERecalcularTotais()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));

            var promocao = new Promocao("Promo Teste", 0.10m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            pedido.AplicarPromocoes(new List<Promocao> { promocao });

            pedido.DescontoPercentual.Should().Be(0.10m);

            pedido.AdicionarProduto(new Item("Batata Frita", 3.00m, TipoItem.Acompanhamento));

            pedido.DescontoPercentual.Should().Be(0m, "o desconto deve ser zerado ao adicionar um novo item");
            pedido.ValorDesconto.Should().Be(0m);
            pedido.Subtotal.Should().Be(8.00m);
            pedido.TotalFinal.Should().Be(8.00m);
        }

        [Fact]
        public void AplicarPromocoes_DeveAplicarOMaiorDesconto_QuandoHouverMultiplasPromocoesComMesmosRequisitos()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 2.00m, TipoItem.Acompanhamento));

            var promo10Porcento = new Promocao("Sanduíche + Batata (10%)", 0.10m);
            promo10Porcento.AdicionarRequisito(TipoItem.Sanduiche);
            promo10Porcento.AdicionarRequisito(TipoItem.Acompanhamento);

            var promo20Porcento = new Promocao("Sanduíche + Batata Especial (20%)", 0.20m);
            promo20Porcento.AdicionarRequisito(TipoItem.Sanduiche);
            promo20Porcento.AdicionarRequisito(TipoItem.Acompanhamento);

            var regrasAtivas = new List<Promocao> { promo10Porcento, promo20Porcento };

            pedido.AplicarPromocoes(regrasAtivas);

            pedido.DescontoPercentual.Should().Be(0.20m);
            pedido.ValorDesconto.Should().Be(1.40m);
            pedido.TotalFinal.Should().Be(5.60m);    
        }

        [Fact]
        public void AplicarPromocoes_NaoDeveAplicarDesconto_QuandoNenhumaPromocaoAtenderAosRequisitos()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Egg", 4.50m, TipoItem.Sanduiche));

            var promocao = new Promocao("Sanduíche + Batata", 0.10m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);

            pedido.AplicarPromocoes(new List<Promocao> { promocao });

            pedido.DescontoPercentual.Should().Be(0m);
            pedido.ValorDesconto.Should().Be(0m);
            pedido.TotalFinal.Should().Be(4.50m);
        }

        [Fact]
        public void AplicarPromocoes_NaoDeveAplicarDesconto_QuandoPedidoTiverItensAMaisQueAPromocao()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 3.00m, TipoItem.Acompanhamento));
            pedido.AdicionarProduto(new Item("Refrigerante", 2.00m, TipoItem.Bebida));

            var promocao = new Promocao("Combo 2 Itens", 0.10m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);

            pedido.AplicarPromocoes(new List<Promocao> { promocao });

            pedido.DescontoPercentual.Should().Be(0m);
            pedido.ValorDesconto.Should().Be(0m);
            pedido.TotalFinal.Should().Be(10.00m);
            pedido.PromocaoId.Should().BeNull();
        }

        [Fact]
        public void AplicarPromocoes_NaoDeveAplicarDesconto_QuandoPedidoTiverMenosItensQueAPromocao()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 3.00m, TipoItem.Acompanhamento));

            var promocao = new Promocao("Super Combo 3 Itens", 0.20m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);
            promocao.AdicionarRequisito(TipoItem.Bebida);

            pedido.AplicarPromocoes(new List<Promocao> { promocao });

            pedido.DescontoPercentual.Should().Be(0m);
            pedido.ValorDesconto.Should().Be(0m);
            pedido.PromocaoId.Should().BeNull();
        }

        [Fact]
        public void AplicarPromocoes_DeveZerarDesconto_QuandoListaDePromocoesDisponiveisForVazia()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 3.00m, TipoItem.Acompanhamento));

            var promocaoOriginal = new Promocao("Combo 2", 0.10m);
            promocaoOriginal.AdicionarRequisito(TipoItem.Sanduiche);
            promocaoOriginal.AdicionarRequisito(TipoItem.Acompanhamento);
            pedido.AplicarPromocoes(new List<Promocao> { promocaoOriginal });

            pedido.DescontoPercentual.Should().Be(0.10m);

            pedido.AplicarPromocoes(new List<Promocao>());

            pedido.DescontoPercentual.Should().Be(0m);
            pedido.ValorDesconto.Should().Be(0m);
            pedido.TotalFinal.Should().Be(8.00m);
            pedido.PromocaoId.Should().BeNull();
        }

        [Fact]
        public void RemoverProduto_DeveRemoverERecalcularTotais_QuandoItemExistir()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Bacon", 7.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Refrigerante", 2.50m, TipoItem.Bebida));

            var sanduicheId = pedido.Itens.First(i => i.Tipo == TipoItem.Sanduiche).Id;

            pedido.RemoverProduto(sanduicheId);

            pedido.Itens.Should().HaveCount(1);
            pedido.Itens.First().Tipo.Should().Be(TipoItem.Bebida);
            pedido.Subtotal.Should().Be(2.50m);
            pedido.TotalFinal.Should().Be(2.50m);
        }

        [Fact]
        public void RemoverProduto_QuandoItemNaoExistir_NaoDeveAlterarOPedido()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Bacon", 7.00m, TipoItem.Sanduiche));
            var idInexistente = Guid.NewGuid();

            pedido.RemoverProduto(idInexistente);

            pedido.Itens.Should().HaveCount(1);
            pedido.Subtotal.Should().Be(7.00m);
        }

        [Fact]
        public void RemoverProduto_QuandoPedidoJaTiverDesconto_DeveZerarDescontoERecalcularTotais()
        {
            var pedido = new Pedido();
            pedido.AdicionarProduto(new Item("X Burger", 5.00m, TipoItem.Sanduiche));
            pedido.AdicionarProduto(new Item("Batata frita", 3.00m, TipoItem.Acompanhamento));

            var promocao = new Promocao("Combo", 0.10m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);
            pedido.AplicarPromocoes(new List<Promocao> { promocao });

            var batataId = pedido.Itens.First(i => i.Tipo == TipoItem.Acompanhamento).Id;

            pedido.RemoverProduto(batataId);

            pedido.DescontoPercentual.Should().Be(0m, "o desconto deve ser zerado ao remover um item do pedido");
            pedido.ValorDesconto.Should().Be(0m);
            pedido.TotalFinal.Should().Be(5.00m);
        }

    }
}
