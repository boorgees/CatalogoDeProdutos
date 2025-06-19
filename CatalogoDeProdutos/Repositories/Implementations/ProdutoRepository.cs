using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
        public async IAsyncEnumerable<Produto> GetProdutoPorCategoria(int id)
        {
            var produtos = _context.Produtos
                .Where(p => p.CategoriaId == id)
                .AsAsyncEnumerable();

            await foreach (var produto in produtos) yield return produto;
        }
    }
}