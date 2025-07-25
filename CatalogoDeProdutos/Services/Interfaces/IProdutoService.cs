using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;

namespace CatalogoDeProdutos.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> ObterTodosAsync();
        Task<ProdutoDTO?> ObterPorIdAsync(int id);
        Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters);
        Task<PagedList<ProdutoDTO>> GetProdutosFiltroAsync(ProdutosFiltroPreco produtosFiltroParams);
        Task<Produto> AdicionarAsync(ProdutoDTO produto);
        Task<ProdutoDTO> AtualizarAsync(ProdutoDTO produto);
        Task RemoverAsync(int id);
        Task<List<ProdutoDTO>> ObterProdutosPorCategoria(int id);
    }
}