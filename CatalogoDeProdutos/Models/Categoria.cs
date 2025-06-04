using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogoDeProdutos.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome da categoria deve conter apenas letras e espaços.")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição da categoria deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição da Categoria")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.!?-]+$", ErrorMessage = "A descrição da categoria pode conter apenas letras, números e alguns caracteres especiais (.,!?-).")]
        public string? Descricao { get; set; }

        [Url(ErrorMessage = "A URL da imagem deve ser um endereço válido.")]
        [Display(Name = "URL da Imagem")]
        [RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "A URL da imagem deve começar com http://, https:// ou ftp://.")]
        [StringLength(300, ErrorMessage = "A URL da imagem deve ter no máximo 300 caracteres.")]
        public string? ImgUrl { get; set; }

        [JsonIgnore]
        [Display(Name = "Produtos")]
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
