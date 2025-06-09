using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto> ObterPorIdAsync(int id);
        Task<Produto> AdicionarAsync(Produto produto);
        Task<Produto> AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
    }
}