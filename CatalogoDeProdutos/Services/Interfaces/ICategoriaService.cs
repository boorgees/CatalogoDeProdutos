using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;

namespace CatalogoDeProdutos.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> ObterTodosAsync();
        Task<CategoriaDTO>? ObterPorIdAsync(int id);
        Task<CategoriaDTO> AdicionarAsync(CategoriaDTO categoria);
        Task<CategoriaDTO> AtualizarAsync(CategoriaDTO categoria);
        Task RemoverAsync(int id);
        Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);
        Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriaFiltroNome categoriasParameters);
    }
}