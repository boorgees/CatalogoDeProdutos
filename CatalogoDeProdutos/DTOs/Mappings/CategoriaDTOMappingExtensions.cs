using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.DTOs.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
        {
            return categoria == null
                ? null
                : new CategoriaDTO
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao,
                    ImgUrl = categoria.ImgUrl
                };
        }

        public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
        {
            return categoriaDTO == null
                ? null
                : new Categoria
                {
                    Id = categoriaDTO.Id,
                    Nome = categoriaDTO.Nome,
                    Descricao = categoriaDTO.Descricao,
                    ImgUrl = categoriaDTO.ImgUrl
                };
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
        {
            if (categorias == null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }

            return categorias.Select(categoria => new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                ImgUrl = categoria.ImgUrl
            }).ToList();
        }
    }
}