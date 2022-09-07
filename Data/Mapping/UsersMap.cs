using Coodesh.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coodesh.Data.Mappings
{
    public class UsersMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("User");

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
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            //Propriedades
            builder.Property(x => x.PasswordHash).IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

        }
    }
}