using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.DTOs.Mappings;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<ProdutoDTO>> ObterProdutosPorCategoriaAsync(int categoriaId)
        {
            return await _context.Produtos
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    ImgUrl = p.ImgUrl,
                    CategoriaId = p.CategoriaId
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<ProdutoDTO>> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = await GetAll()
                .OrderBy(p => p.Nome)
                .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
                .Take(produtosParameters.PageSize)
                .ToListAsync();

            return produtos.ToProdutoDTOList();
        }
    }
}