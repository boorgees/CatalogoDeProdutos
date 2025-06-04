using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Produto")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "O nome do produto deve conter apenas letras, números e espaços.")]
        public string Nome { get; set; }

        [StringLength(300, ErrorMessage = "A descrição do produto deve ter no máximo 300 caracteres.")]
        [Display(Name = "Descrição do Produto")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.!?-]+$", ErrorMessage = "A descrição do produto pode conter apenas letras, números e alguns caracteres especiais (.,!?-).")]
        public string? Descricao { get; set; }

        [Url(ErrorMessage = "A URL da imagem deve ser um endereço válido.")]
        [Display(Name = "URL da Imagem")]
        [RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "A URL da imagem deve começar com http://, https:// ou ftp://.")]
        [StringLength(300, ErrorMessage = "A URL da imagem deve ter no máximo 300 caracteres.")]
        public string? ImgUrl { get; set; }


        public DateTime DataDeCriacao { get; set; } = DateTime.Now;


        public int CategodriaId { get; set; }
        [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
        [Display(Name = "Categoria do Produto")]
        public Categoria categoria { get; set; }
    }
}
