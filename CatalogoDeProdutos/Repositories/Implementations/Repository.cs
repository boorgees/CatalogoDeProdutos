using System.Linq.Expressions;
using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<T> GetAll()
        {
            await foreach (var item in _context.Set<T>().AsNoTracking().AsAsyncEnumerable()) yield return item;
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            // await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            // _context.Entry(entity).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            // await _context.SaveChangesAsync();
            return entity;
        }
    }
}