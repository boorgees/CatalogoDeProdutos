using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController(IUnityOfWork uof) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> ObterTodosAsync()
        {
            var categorias = await uof.CategoriaService.ObterTodosAsync();

            if (categorias == null || !categorias.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoriaPorId")]
        public async Task<ActionResult<Categoria>> ObterPorIdAsync(int id)
        {
            var categoria = await uof.CategoriaService.ObterPorIdAsync(id)!;

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> AdicionarAsync([FromBody] Categoria categoria)
        {
            try
            {
                await uof.CategoriaService.AdicionarAsync(categoria);
                uof.Commit();
                var categoriaCriada = await uof.CategoriaService.ObterPorIdAsync(categoria.Id);

                return CreatedAtRoute("ObterCategoriaPorId", new { id = categoriaCriada.Id }, categoriaCriada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro, contate o servidor. Detalhe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            try
            {
                await uof.CategoriaService.AtualizarAsync(categoria);
                uof.Commit();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            try
            {
                await uof.CategoriaService.RemoverAsync(id);
                uof.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }
    }
}