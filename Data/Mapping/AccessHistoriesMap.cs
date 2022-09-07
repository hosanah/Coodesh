using Coodesh.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coodesh.Data.Mappings
{
    public class AccessHistoriesMap : IEntityTypeConfiguration<AccessHistory>
    {
        public void Configure(EntityTypeBuilder<AccessHistory> builder)
        {
            // Tabela
            builder.ToTable("AccessHistory");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.AccessedWhen)
                .IsRequired()
                .HasColumnName("AccessedWhen")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETDATE()");

            // Relacionamentos
            builder
                .HasOne(x => x.Who)
                .WithMany(x => x.AccessHistories)
                .HasConstraintName("FK_Access_User")
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}