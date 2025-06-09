using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogoDeProdutos.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImgUrl { get; set; }

        [JsonIgnore]
        [Display(Name = "Produtos")]
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
