namespace Estoque.Models
{
    public class MovimentoEstoqueInclusaoEvento
    {
       public int IdProduto { get; set; }
       public int Quantidade { get; set; }
       public string? Tipo { get; set; }
    }
}