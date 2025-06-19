using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IUnityOfWork
    {
        // obtem instancias dos repositorios
        // e da possibilidade de metodos personalizados
        IProdutoService ProdutoService { get; }
        ICategoriaService CategoriaService { get; }

        // metodo para realizar a persistencia
        void Commit();
        void Dispose();
    }
}