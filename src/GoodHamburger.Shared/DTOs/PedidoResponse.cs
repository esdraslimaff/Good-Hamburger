namespace GoodHamburger.Shared.DTOs
{
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public Guid? PromocaoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemCardapioDto> Itens { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal DescontoPercentual { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal TotalFinal { get; set; }
    }
}
