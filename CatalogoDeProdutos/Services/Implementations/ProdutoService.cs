using System.Linq.Expressions;
using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Services.Implementations
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            var produtos = new List<Produto>();
            await foreach (var produto in produtoRepository.GetAll()) produtos.Add(produto);
            return produtos;
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            Expression<Func<Produto, bool>> predicate = p => p.Id == id;
            var produto = await produtoRepository.GetById(predicate);
            return produto;
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                throw new Exception("Campos obrigatórios não preenchidos.");
            }

            try
            {
                return await produtoRepository.Create(produto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Produto> AtualizarAsync(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            if (produto.Id <= 0)
            {
                throw new ArgumentException("Id do produto inválido");
            }

            Expression<Func<Produto, bool>> predicate = p => p.Id == produto.Id;
            var produtoExistente = await produtoRepository.GetById(predicate);
            if (produtoExistente == null)
            {
                throw new Exception($"Produto com ID {produto.Id} não encontrado");
            }

            try
            {
                produtoExistente.Nome = !string.IsNullOrEmpty(produto.Nome)
                    ? produto.Nome
                    : produtoExistente.Nome;

                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.ImgUrl = produto.ImgUrl;

                return await produtoRepository.Update(produtoExistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto", ex);
            }
        }

        public async Task RemoverAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id do produto inválido");
            }

            Expression<Func<Produto, bool>> predicate = p => p.Id == id;
            var produto = await produtoRepository.GetById(predicate);
            if (produto == null)
            {
                return;
            }

            try
            {
                await produtoRepository.Delete(produto);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover produto", ex);
            }
        }

        public async IAsyncEnumerable<ProdutoDTO?> GetProdutoPorCategoria(int id)
        {
            await foreach (var produto in produtoRepository.GetProdutoPorCategoria(id))
                if (produto.CategoriaId == id)
                {
                    yield return produto;
                }
        }
    }
}