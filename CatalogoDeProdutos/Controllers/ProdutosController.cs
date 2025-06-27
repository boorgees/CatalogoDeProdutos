using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(IUnityOfWork uof) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ObterTodosAsync() // OK
        {
            var produtos = await uof.ProdutoService.ObterTodosAsync();

            if (produtos == null || !produtos.Any())
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}/categorias", Name = "ObterProdutosPorCategoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ObterProdutosPorCategoriaAsync(int id) // OK
        {
            if (id <= 0)
            {
                return BadRequest("ID da categoria inválido.");
            }

            var produtosDaCategoria = await uof.ProdutoService.ObterProdutosPorCategoria(id);

            if (produtosDaCategoria == null || !produtosDaCategoria.Any())
            {
                return NotFound($"Nenhum produto encontrado para a categoria {id}.");
            }

            return Ok(produtosDaCategoria);
        }


        [HttpGet("{id:int}", Name = "ObterProdutoPorId")] // OK
        public async Task<ActionResult<ProdutoDTO>> ObterPorIdAsync(int id)
        {
            var produto = await uof.ProdutoService.ObterPorIdAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> AdicionarAsync([FromBody] ProdutoDTO produto) // OK  
        {
            try
            {
                var produtoEntity = await uof.ProdutoService.AdicionarAsync(produto);
                uof.Commit();

                var produtoCriado = new ProdutoDTO
                {
                    Id = produtoEntity.Id,
                    Nome = produtoEntity.Nome,
                    Descricao = produtoEntity.Descricao,
                    ImgUrl = produtoEntity.ImgUrl,
                    CategoriaId = produtoEntity.CategoriaId
                };

                return CreatedAtRoute("ObterProdutoPorId", new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro, contate o servidor. Detalhe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produto) // OK
        {
            try
            {
                await uof.ProdutoService.AtualizarAsync(produto);
                uof.Commit();
                return Ok(produto);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id) // OK
        {
            try
            {
                await uof.ProdutoService.RemoverAsync(id);
                uof.Commit();
                return Ok("Produto removido com sucesso.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}