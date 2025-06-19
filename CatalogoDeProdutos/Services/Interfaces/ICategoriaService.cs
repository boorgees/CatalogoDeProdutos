using System.Linq.Expressions;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObterTodosAsync();
        Task<Categoria>? ObterPorIdAsync(Expression<Func<Categoria, bool>> id);
        Task<Categoria> AdicionarAsync(Categoria categoria);
        Task<Categoria> AtualizarAsync(Categoria categoria);
        Task RemoverAsync(int id);
    }
}