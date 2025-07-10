using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<ProdutoDTO>> ObterProdutosPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<ProdutoDTO>> GetProdutos(ProdutosParameters produtosParameters);
    }
}