﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo2.Domain.Entities;

namespace ToDo2.Infra.Mappings;

public class TasksMapping : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(c => c.Descricao)
            .HasMaxLength(200);

        builder.Property(c => c.Expiracao);

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.Property(c => c.AtualizadoEm)
            .IsRequired();
    }
}