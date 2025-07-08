using AutoMapper;
using CatalogoDeProdutos.Models;

namespace CatalogoDeProdutos.DTOs.Mappings
{
    public class DomainDtoMappingProfile : Profile
    {
        public DomainDtoMappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}