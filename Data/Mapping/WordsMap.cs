using Coodesh.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coodesh.Data.Mappings
{
    public class WordsMap : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            // Tabela
            builder.ToTable("Word");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

             // Propriedades
            builder.Property(x => x.Favorite)
                .IsRequired()
                .HasColumnName("Favorite")
                .HasColumnType("NUMERIC")
                .HasMaxLength(1);

        }
    }
}