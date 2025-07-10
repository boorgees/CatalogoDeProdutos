using System.Linq.Expressions;
using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.DTOs.Mappings;
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
        public async Task<IEnumerable<CategoriaDTO>> ObterTodosAsync()
        {
            var categorias = new List<Categoria>();
            await foreach (var categoria in _categoriaRepository.GetAll()) categorias.Add(categoria);

            if (!categorias.Any())
            {
                throw new Exception("Nenhuma categoria encontrada.");
            }

            var categoriasDto = categorias.ToCategoriaDTOList();

            return categoriasDto;
        }

        public async Task<CategoriaDTO>? ObterPorIdAsync(int id)
        {
            Expression<Func<Categoria, bool>> predicate = p => p.Id == id;
            var categoria = await _categoriaRepository.GetById(predicate);

            if (categoria == null)
            {
                return null;
            }

            var categoriaDto = categoria.ToCategoriaDTO();

            return categoriaDto;
        }

        public async Task<CategoriaDTO> AdicionarAsync(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
            {
                throw new ArgumentNullException(nameof(categoriaDto));
            }

            var categoria = categoriaDto.ToCategoria();

            try
            {
                await _categoriaRepository.Create(categoria);

                var categoriaCriadaDto = categoria.ToCategoriaDTO();
                return categoriaCriadaDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar categoria.", ex);
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

        public async Task<CategoriaDTO> AtualizarAsync(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
            {
                throw new ArgumentNullException(nameof(categoriaDto));
            }

            var categoriaExistente = await _categoriaRepository.GetById(c => c.Id == categoriaDto.Id);

            if (categoriaExistente is null)
            {
                throw new Exception($"Categoria com ID {categoriaDto.Id} não encontrada.");
            }

            try
            {
                categoriaExistente.Nome = categoriaDto.Nome;
                categoriaExistente.Descricao = categoriaDto.Descricao;
                categoriaExistente.ImgUrl = categoriaDto.ImgUrl;


                await _categoriaRepository.Update(categoriaExistente);
                return categoriaDto;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar categoria.", e);
            }
        }
    }
}