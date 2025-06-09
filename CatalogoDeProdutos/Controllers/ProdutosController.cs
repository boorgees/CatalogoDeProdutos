using CatalogoDeProdutos.Data;
using Microsoft.AspNetCore.Mvc;
using CatalogoDeProdutos.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodosAsync()
        {
            var produtos = await _context.Produtos
                .AsNoTracking()
                .ToListAsync();

            if (!produtos.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> ObterPorIdAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            
            return produto == null ? NotFound("Produto não encontrado.") : Ok(produto);
        }

        
    }
}