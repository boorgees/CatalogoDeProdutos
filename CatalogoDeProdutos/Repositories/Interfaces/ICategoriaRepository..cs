using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface ICategoriaRepository_
    {
        Task<IEnumerable<Categoria?>> ObterTodosAsync();
        Task<Categoria>? ObterPorIdAsync(int id);
        Task<Categoria> AdicionarAsync(Categoria categoria);
        Task<Categoria> AtualizarAsync(Categoria categoria);
        Task? RemoverAsync(int id);
    }
}