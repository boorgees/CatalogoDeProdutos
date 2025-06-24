using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> ObterTodosAsync();
        Task<CategoriaDTO>? ObterPorIdAsync(int id);
        Task<Categoria> AdicionarAsync(CategoriaDTO categoria);
        Task<CategoriaDTO> AtualizarAsync(CategoriaDTO categoria);
        Task RemoverAsync(int id);
    }
}