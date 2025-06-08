using CatalogoDeProdutos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoDeProdutos.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
          
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired() 
                .HasMaxLength(100);

            builder.Property(c => c.Descricao)
                .HasMaxLength(500);

            builder.Property(c => c.ImgUrl)
                .HasMaxLength(300);

            builder.HasMany(c => c.Produtos)
                   .WithOne(p => p.Categoria)
                   .HasForeignKey(p => p.CategoriaId);
        }
    }
}