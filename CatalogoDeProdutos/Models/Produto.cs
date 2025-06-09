namespace CatalogoDeProdutos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}