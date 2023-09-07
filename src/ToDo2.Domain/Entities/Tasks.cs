﻿namespace ToDo2.Domain.Entities;

public class Tasks : BaseEntity
{
    public int UserId { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public DateTime Expiracao { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    public virtual Users User { get; set; } = new();
}