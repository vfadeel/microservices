namespace Estoque.Models
{
    public class MovimentoEstoque
    {
        public int IdMovimentoEstoque { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public string? Tipo { get; set; }
    }
}