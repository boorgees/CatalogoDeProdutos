using System.Linq.Expressions;
using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.DTOs.Mappings;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Pagination;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Services.Implementations
{
    public class ProdutoService : IProdutoService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;


        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterTodosAsync() // OK
        {
            var produtos = new List<Produto>();
            try
            {
                await foreach (var produto in _produtoRepository.GetAll())
                    produtos.Add(produto);
            }
            catch (Exception)
            {
                throw new Exception("Nenhum produto encontrado.");
            }

            var produtosDto = produtos.ToProdutoDTOList();

            return produtosDto;
        }

        public async Task<ProdutoDTO?> ObterPorIdAsync(int id)
        {
            Expression<Func<Produto, bool>> predicate = p => p.Id == id;
            var produto = await _produtoRepository.GetById(predicate);

            if (produto == null)
            {
                return null;
            }

            var produtosDto = produto.ToProdutoDTO();

            return produtosDto;
        }


        public async Task RemoverAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id do produto inválido");
            }

            Expression<Func<Produto, bool>> predicate = p => p.Id == id;
            var produto = await _produtoRepository.GetById(predicate);
            if (produto == null)
            {
                throw new ArgumentNullException($"O produto com ID {id} não existe");
            }

            try
            {
                await _produtoRepository.Delete(produto);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover produto", ex);
            }
        }

        public async Task<Produto> AdicionarAsync(ProdutoDTO produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                throw new Exception("Campos obrigatórios não preenchidos");
            }

            if (produto == null)
            {
                throw new NullReferenceException("O produto não está preenchido");
            }

            var produtoDto = produto.ToProduto();

            try
            {
                await _produtoRepository.Create(produtoDto);
                return produtoDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProdutoDTO> AtualizarAsync(ProdutoDTO produto)
        {
            if (produto.Id <= 0 || produto == null)
            {
                throw new ArgumentException("Id do produto inválido");
            }

            if (produto.CategoriaId <= 0)
            {
                throw new ArgumentException("Id da categoria inválido");
            }

            var categoria = await _categoriaRepository.GetById(c => c.Id == produto.CategoriaId);
            if (categoria == null)
            {
                throw new InvalidOperationException($"A categoria com ID {produto.CategoriaId} não existe");
            }

            Expression<Func<Produto, bool>> predicate = p => p.Id == produto.Id;
            var produtoExistente = await _produtoRepository.GetById(predicate);
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
                produtoExistente.CategoriaId = produto.CategoriaId;

                await _produtoRepository.Update(produtoExistente);
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto", ex);
            }
        }

        public async Task<List<ProdutoDTO>> ObterProdutosPorCategoria(int id)
        {
            var produtosDaCategoria = new List<ProdutoDTO>();

            try
            {
                var categoria = await _categoriaRepository.GetById(c => c.Id == id);
                if (categoria.Id != null && categoria.Id > 0)
                {
                    produtosDaCategoria = await _produtoRepository.ObterProdutosPorCategoriaAsync(categoria.Id);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao obter produtos por categoria", ex);
            }

            return produtosDaCategoria;
        }
        public async Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters)
        {
            return await _produtoRepository.GetProdutosAsync(produtosParameters);
        }
    }
}