using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class CategoriaRepository : ICategoriaRepository_
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Categoria?>> ObterTodosAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }
        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            var categoria =  await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            return categoria;
        }
        
        public async Task<Categoria> AdicionarAsync(Categoria categoria)
        {
            var categoriaAdicionada = await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoriaAdicionada.Entity;
        }
        
        public async Task<Categoria> AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
        public Task? RemoverAsync(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}