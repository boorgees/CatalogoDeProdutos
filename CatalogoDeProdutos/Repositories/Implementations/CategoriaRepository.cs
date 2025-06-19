using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class CategoriaRepository: Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context){}
    }
}