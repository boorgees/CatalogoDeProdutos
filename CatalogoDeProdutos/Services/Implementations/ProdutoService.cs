using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Implementations;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Services.Implementations
{
    public class ProdutoService (IProdutoRepository produtoRepository) : IProdutoService
    {
        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            var produtos = await produtoRepository.ObterTodosAsync();
            if (!produtos.Any())
            {
                throw new Exception("Nenhum produto encontrado.");
            }

            return produtos;
        }
        
        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            var produto = await produtoRepository.ObterPorIdAsync(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            return produto;
        }
        
        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            var produtoNovo = await produtoRepository.AdicionarAsync(produto);
            if (produtoNovo.Nome == null )
            {
                throw new Exception("Campos obrigatórios não preenchidos.");
            }

            try
            {
                await produtoRepository.AdicionarAsync(produtoNovo);
                return produtoNovo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public async Task<Produto> AtualizarAsync(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (produto.Id <= 0)
                throw new ArgumentException("Id do produto inválido");
            
            var produtoExistente = await produtoRepository.ObterPorIdAsync(produto.Id);
            if (produtoExistente == null)
                throw new Exception($"Produto com ID {produto.Id} não encontrado");

            try
            {
                produtoExistente.Nome = !string.IsNullOrEmpty(produto.Nome) 
                    ? produto.Nome 
                    : produtoExistente.Nome;
            
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.ImgUrl = produto.ImgUrl;

                await produtoRepository.AtualizarAsync(produtoExistente);
                return produtoExistente;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto", ex);
            }
        }
        
        public async Task RemoverAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id do produto inválido");

            var produto = await produtoRepository.ObterPorIdAsync(id);
            if (produto == null)
                throw new Exception($"Produto com ID {id} não encontrado");

            try
            {
                await produtoRepository.RemoverAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover produto", ex);
            }
        }
    }
}