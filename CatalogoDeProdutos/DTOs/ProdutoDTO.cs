using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.DTOs
{
    public class ProdutoDTO
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoriaId { get; set; }
    }
}