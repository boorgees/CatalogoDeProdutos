using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class ProdutoRepository(AppDbContext context) : IProdutoRepository
    {
        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            var produtos = await context.Produtos.ToListAsync();
            return produtos;
        }
        
        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            return produto;
        }
        
        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            var produtoAdicionado = await context.Produtos.AddAsync(produto);
            return produtoAdicionado.Entity;
        }
        
        public Task<Produto> AtualizarAsync(Produto produto)
        {
            context.Produtos.Update(produto);
            return Task.FromResult(produto);
        }
        
        public async Task RemoverAsync(int id)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto != null)
            {
                context.Produtos.Remove(produto);
            }
        }
    }
}