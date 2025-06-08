using CatalogoDeProdutos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoDeProdutos.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descricao)
                .HasMaxLength(300);

            builder.Property(p => p.ImgUrl)
                .HasMaxLength(300);


            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .IsRequired();
        }
    }
}