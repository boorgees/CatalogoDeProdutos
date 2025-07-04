using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.DTOs.Mappings
{
    public static class ProdutoDTOMappingExtensions
    {
        public static ProdutoDTO? ToProdutoDTO(this Produto produto)
        {
            return produto == null
                ? null
                : new ProdutoDTO
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    ImgUrl = produto.ImgUrl,
                    CategoriaId = produto.CategoriaId
                };
        }

        public static Produto? ToProduto(this ProdutoDTO produtoDto)
        {
            return produtoDto == null
                ? null
                : new Produto
                {
                    Id = produtoDto.Id,
                    Nome = produtoDto.Nome,
                    Descricao = produtoDto.Descricao,
                    ImgUrl = produtoDto.ImgUrl,
                    CategoriaId = produtoDto.CategoriaId
                };
        }

        public static IEnumerable<ProdutoDTO> ToProdutoDTOList(this IEnumerable<Produto> produtos)
        {
            if (produtos == null || !produtos.Any())
            {
                return new List<ProdutoDTO>();
            }

            return produtos.Select(produto => new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ImgUrl = produto.ImgUrl
            }).ToList();
        }
    }
}