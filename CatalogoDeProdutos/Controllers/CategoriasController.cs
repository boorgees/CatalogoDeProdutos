using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Pagination;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            try
            {
                var categorias = await uof.CategoriaService.GetCategoriasAsync(categoriasParameters);

                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasNext,
                    categorias.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(categorias);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possivel obter as categorias.");
            }
        }

        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetByNome([FromQuery] CategoriaFiltroNome categoriaFiltroParams)
        {
            var categorias = await uof.CategoriaService.GetCategoriasFiltroNome(categoriaFiltroParams);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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