using CatalogoDeProdutos.DTOs;
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
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ObterTodosAsync() // OK
        {
            var categorias = await uof.CategoriaService.ObterTodosAsync();

            if (categorias == null || !categorias.Any())
            {
                return NotFound("Nenhuma categoria encontrada.");
            }

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoriaPorId")] // OK
        public async Task<ActionResult<CategoriaDTO>> ObterPorIdAsync(int id)
        {
            var categoria = await uof.CategoriaService.ObterPorIdAsync(id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> AdicionarAsync([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoriaEntity = await uof.CategoriaService.AdicionarAsync(categoriaDto);
                uof.Commit();

                var categoriaCriadaDto = new CategoriaDTO
                {
                    Id = categoriaEntity.Id,
                    Nome = categoriaEntity.Nome,
                    Descricao = categoriaEntity.Descricao,
                    ImgUrl = categoriaEntity.ImgUrl
                };

                return CreatedAtRoute("ObterCategoriaPorId", new { id = categoriaCriadaDto.Id }, categoriaCriadaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro, contate o servidor. Detalhe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")] // OK
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest("O id da requisição não corresponde ao id do objeto.");
            }

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

        [HttpDelete("{id:int}")] // OK
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
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