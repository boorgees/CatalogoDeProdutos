using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IAsyncEnumerable<ProdutoDTO?> GetProdutoPorCategoria(int id);
    }
}