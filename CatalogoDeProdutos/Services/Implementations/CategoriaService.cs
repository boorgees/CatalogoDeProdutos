using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Implementations;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Services.Implementations
{
    public class CategoriaService (ICategoriaRepository categoriaRepository) : ICategoriaService
    {
        // private readonly CategoriaRepository _categoriaRepository;
        //
        // public CategoriaService(CategoriaRepository categoriaRepository)
        // {
        //     _categoriaRepository = categoriaRepository;
        // }

        public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        {
            var categorias = await categoriaRepository.ObterTodosAsync();

            if (!categorias.Any())
            {
                throw new Exception("Nenhuma categoria encontrada.");
            }

            return categorias;
        }
        
        public async Task<Categoria>? ObterPorIdAsync(int id)
        {
            var categoria = await categoriaRepository.ObterPorIdAsync(id);

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
                await categoriaRepository.AdicionarAsync(categoria);
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

            var categoriaExistente = await categoriaRepository.ObterPorIdAsync(categoria.Id);
           
            try
            {
                categoriaExistente.Nome = !string.IsNullOrEmpty(categoria.Nome) 
                    ? categoria.Nome
                    : categoriaExistente.Nome;
                
                categoriaExistente.Descricao = categoria.Descricao;
                categoriaExistente.ImgUrl = categoria.ImgUrl;
                
                await categoriaRepository.AtualizarAsync(categoriaExistente);
                return categoriaExistente;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar categoria.", e);
            }
        }
        
        public async Task RemoverAsync(int id)
        {
            var categoria = await categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                throw new Exception($"Categoria com ID {id} não encontrada.");
            }

            try
            {
                await categoriaRepository.RemoverAsync(categoria.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover categoria.", e);
            }
        }
    }
}