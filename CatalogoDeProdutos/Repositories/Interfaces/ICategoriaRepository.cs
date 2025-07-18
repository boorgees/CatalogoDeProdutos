using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);
        Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriaFiltroNome categoriasParameters);
    }
}