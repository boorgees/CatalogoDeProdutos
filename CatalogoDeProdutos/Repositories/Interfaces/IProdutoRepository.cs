using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IAsyncEnumerable<Produto?> GetProdutoPorCategoria(int id);
    }
}