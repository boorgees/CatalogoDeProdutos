using System.Linq.Expressions;

namespace CatalogoDeProdutos.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(Expression<Func<T, bool>> predicate);
        T Create (T entity);
        T Update(T entity);
        T Delete (T entity);
    }
}