using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class CategoriaRepository(AppDbContext context) : ICategoriaRepository_
    {
        public async Task<IEnumerable<Categoria?>> ObterTodosAsync()
        {
            var categorias = await context.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            return categoria;
        }
        
        public async Task<Categoria> AdicionarAsync(Categoria categoria)
        {
            var categoriaAdicionada = await context.Categorias.AddAsync(categoria);
            await context.SaveChangesAsync();
            return categoriaAdicionada.Entity;
        }
        
        public async Task<Categoria> AtualizarAsync(Categoria categoria)
        {
            context.Categorias.Update(categoria);
            await context.SaveChangesAsync();
            return categoria;
        }

        public async Task RemoverAsync(int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria != null)
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
            }
        }
    }
}