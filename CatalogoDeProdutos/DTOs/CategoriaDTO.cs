using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.DTOs
{
    public class CategoriaDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImgUrl { get; set; }
    }
}