namespace GoodHamburger.BlazorWasm.Models
{
    public class PedidoResumo
    {
        public decimal Subtotal { get; set; }
        public decimal PercentualDesconto { get; set; }
        public string NomePromocaoAtiva { get; set; }
        public decimal ValorDesconto => Subtotal * PercentualDesconto;
        public decimal TotalFinal => Subtotal - ValorDesconto;
    }
}
