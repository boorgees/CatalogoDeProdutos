using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<ProdutoDTO>> ObterProdutosPorCategoriaAsync(int categoriaId);
    }
}