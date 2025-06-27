using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> ObterTodosAsync();
        Task<ProdutoDTO?> ObterPorIdAsync(int id);
        Task<Produto> AdicionarAsync(ProdutoDTO produto);
        Task<ProdutoDTO> AtualizarAsync(ProdutoDTO produto);
        Task RemoverAsync(int id);
        Task<List<ProdutoDTO>> ObterProdutosPorCategoria(int id);
    }
}