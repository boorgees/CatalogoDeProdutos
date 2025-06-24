using CatalogoDeProdutos.Data;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Implementations;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Repositories.Implementations
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly AppDbContext _context;
        private readonly IProdutoRepository _produtoRepository;
        private ICategoriaService? _categoriaService;
        private IProdutoService? _produtoService;

        public UnityOfWork(
            AppDbContext context,
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _context = context;
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IProdutoService ProdutoService
        {
            get { return _produtoService = _produtoService ?? new ProdutoService(_produtoRepository); }
        }

        public ICategoriaService CategoriaService
        {
            get { return _categoriaService ??= new CategoriaService(_categoriaRepository); }
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}