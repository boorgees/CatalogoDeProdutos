using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<ProdutoDTO>> ObterProdutosPorCategoriaAsync(int categoriaId);
        Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters);
        Task<PagedList<ProdutoDTO>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    }
}