using System.Linq.Expressions;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        {
            var categorias = new List<Categoria>();
            await foreach (var categoria in _categoriaRepository.GetAll()) categorias.Add(categoria);

            if (!categorias.Any())
            {
                throw new Exception("Nenhuma categoria encontrada.");
            }

            return categorias;
        }

        public async Task<Categoria>? ObterPorIdAsync(Expression<Func<Categoria, bool>> id)
        {
            var categoria = await _categoriaRepository.GetById(id);

            if (categoria == null)
            {
                return null;
            }

            return categoria;
        }
        public async Task<Categoria> AdicionarAsync(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            try
            {
                await _categoriaRepository.Update(categoria);
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar categoria.", ex);
            }
        }

        public async Task<Categoria> AtualizarAsync(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            var categoriaExistente = await _categoriaRepository.GetById(c => c.Id == categoria.Id);

            try
            {
                categoriaExistente.Nome = !string.IsNullOrEmpty(categoria.Nome)
                    ? categoria.Nome
                    : categoriaExistente.Nome;

                categoriaExistente.Descricao = categoria.Descricao;
                categoriaExistente.ImgUrl = categoria.ImgUrl;

                await _categoriaRepository.Update(categoriaExistente);
                return categoriaExistente;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar categoria.", e);
            }
        }

        public async Task RemoverAsync(int id)
        {
            var categoria = await _categoriaRepository.GetById(c => c.Id == id);
            if (categoria == null)
            {
                throw new Exception($"Categoria com ID {id} não encontrada.");
            }

            try
            {
                await _categoriaRepository.Delete(categoria);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover categoria.", e);
            }
        }
    }
}