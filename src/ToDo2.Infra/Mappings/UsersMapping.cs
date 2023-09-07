using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo2.Domain.Entities;

namespace ToDo2.Infra.Mappings;

public class UsersMapping : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(c => c.Senha)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.HasMany(c => c.TasksList)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}