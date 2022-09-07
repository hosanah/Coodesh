using Coodesh.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coodesh.Data.Mappings
{
    public class FavoriteWordsMap : IEntityTypeConfiguration<FavoriteWord>
    {
        public void Configure(EntityTypeBuilder<FavoriteWord> builder)
        {
            // Tabela
            builder.ToTable("FavoriteWord");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.When)
                .IsRequired()
                .HasColumnName("When")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETDATE()");

            // Relacionamentos
            builder
                .HasOne(x => x.Who)
                .WithMany(x => x.FavoriteWords)
                .HasConstraintName("FK_Favorite_User")
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamentos
            builder
                .HasOne(x => x.Word)
                .WithMany(x => x.FavoriteWords)
                .HasConstraintName("FK_Favorite_Word")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}