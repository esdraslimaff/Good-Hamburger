using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Tests.Entities
{
    public class PromocaoTests
    {

        [Fact]
        public void Construtor_QuandoParametrosForemValidos_DeveCriarEntidadeCorretamente()
        {
            var nomeEsperado = "Sanduíche + Batata + Refrigerante";
            var percentualEsperado = 0.20m;

            var promocao = new Promocao(nomeEsperado, percentualEsperado);

            promocao.Should().NotBeNull();
            promocao.Nome.Should().Be(nomeEsperado);
            promocao.Percentual.Should().Be(percentualEsperado);
            promocao.Ativo.Should().BeTrue();
            promocao.Requisitos.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-50)]
        [InlineData(1.1)]
        [InlineData(2)]
        public void Construtor_QuandoPercentualForInvalido_DeveLancarExcecao(decimal percentualInvalido)
        {
            Action acao = () => new Promocao("Promo", percentualInvalido);
            acao.Should().Throw<ArgumentException>()
                .WithMessage("*percentual de desconto deve estar entre 0 e 1*");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Construtor_QuandoNomeForVazioOuNulo_DeveLancarExcecao(string nomeInvalido)
        {
            Action acao = () => new Promocao(nomeInvalido, 0.10m);
            acao.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AdicionarRequisito_QuandoItemForNovo_DeveAdicionarItemAListaDeRequisitos()
        {
            
            var promocao = new Promocao("Promoção Teste", 0.10m);
          
            promocao.AdicionarRequisito(TipoItem.Sanduiche);

            promocao.Requisitos.Should().HaveCount(1);
            promocao.Requisitos.Should().Contain(r => r.TipoItem == TipoItem.Sanduiche);
        }

        [Fact]
        public void AdicionarRequisito_QuandoItemJaExistir_NaoDeveDuplicarRequisito()
        {
            
            var promocao = new Promocao("Promoção Teste", 0.10m);

            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
          
            promocao.Requisitos.Should().HaveCount(1, "o método não deve permitir itens duplicados na mesma promoção");
        }

        [Fact]
        public void ContemTodosRequisitos_QuandoPedidoContemExatamenteOsRequisitos_DeveRetornarTrue()
        {
            var promocao = new Promocao("Combo Completo", 0.20m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);

            var itensDoPedido = new List<TipoItem>
            {
                TipoItem.Sanduiche,
                TipoItem.Acompanhamento
            };

            var resultado = promocao.ContemTodosRequisitos(itensDoPedido);

            resultado.Should().BeTrue();
        }

        [Fact]
        public void ContemTodosRequisitos_QuandoPedidoFaltaRequisito_DeveRetornarFalse()
        {
            var promocao = new Promocao("Combo", 0.20m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Acompanhamento);

            var itensDoPedido = new List<TipoItem> { TipoItem.Sanduiche };

            var resultado = promocao.ContemTodosRequisitos(itensDoPedido);

            resultado.Should().BeFalse();
        }

        [Fact]
        public void ContemTodosRequisitos_QuandoPedidoTiverMaisItensQueORequisito_DeveRetornarTrue()
        {
            var promocao = new Promocao("Promo 2 itens", 0.15m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);
            promocao.AdicionarRequisito(TipoItem.Bebida);

            var itensDoPedido = new List<TipoItem>
            {
                TipoItem.Sanduiche,
                TipoItem.Bebida,
                TipoItem.Acompanhamento
            };

            var resultado = promocao.ContemTodosRequisitos(itensDoPedido);

            resultado.Should().BeTrue();
        }

        [Fact]
        public void ContemTodosRequisitos_QuandoListaPedidoForVaziaOuNula_DeveRetornarFalse()
        {
            var promocao = new Promocao("Combo", 0.10m);
            promocao.AdicionarRequisito(TipoItem.Sanduiche);

            promocao.ContemTodosRequisitos(new List<TipoItem>()).Should().BeFalse("lista vazia não atende requisitos");
            promocao.ContemTodosRequisitos(null).Should().BeFalse("lista nula não atende requisitos e não deve quebrar a aplicação");
        }

        [Fact]
        public void ContemTodosRequisitos_QuandoPromocaoNaoPossuirRequisitos_DeveRetornarFalse()
        {
            var promocao = new Promocao("Promoção Fantasma", 0.10m);
            var itensDoPedido = new List<TipoItem> { TipoItem.Sanduiche };

            var resultado = promocao.ContemTodosRequisitos(itensDoPedido);

            resultado.Should().BeFalse("uma promoção sem requisitos configurados não pode ser aplicada");
        }
    }
}
