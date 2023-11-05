using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SitemaProjeto.Models;

namespace SitemaProjeto.Data;

public partial class SistemaProjetoContext : DbContext
{
    public SistemaProjetoContext()
    {
    }

    public SistemaProjetoContext(DbContextOptions<SistemaProjetoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Candidatura> Candidaturas { get; set; }

    public virtual DbSet<Edicao> Edicaos { get; set; }

    public virtual DbSet<Externo> Externos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Interno> Internos { get; set; }

    public virtual DbSet<Orientador> Orientadors { get; set; }

    public virtual DbSet<Orientum> Orienta { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=SistemaProjetoContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.IdAluno).HasName("PK__Aluno__06D5E4761F943BAA");

            entity.HasMany(d => d.IdGrupos).WithMany(p => p.IdAlunos)
                .UsingEntity<Dictionary<string, object>>(
                    "Pertence",
                    r => r.HasOne<Grupo>().WithMany()
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Pertence__ID_Gru__403A8C7D"),
                    l => l.HasOne<Aluno>().WithMany()
                        .HasForeignKey("IdAluno")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Pertence__ID_Alu__3F466844"),
                    j =>
                    {
                        j.HasKey("IdAluno", "IdGrupo").HasName("PK__Pertence__FE5331A80B303854");
                        j.ToTable("Pertence");
                        j.IndexerProperty<int>("IdAluno").HasColumnName("ID_Aluno");
                        j.IndexerProperty<int>("IdGrupo").HasColumnName("ID_Grupo");
                    });
        });

        modelBuilder.Entity<Candidatura>(entity =>
        {
            entity.HasKey(e => new { e.IdGrupo, e.IdProjeto }).HasName("PK__Candidat__994EB61DBEB601F1");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Candidaturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidatu__ID_Gr__37A5467C");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Candidaturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidatu__ID_pr__38996AB5");
        });

        modelBuilder.Entity<Edicao>(entity =>
        {
            entity.HasKey(e => e.IdEdicao).HasName("PK__Edicao__FB4794052FD7C2F0");
        });

        modelBuilder.Entity<Externo>(entity =>
        {
            entity.HasKey(e => e.IdExterno).HasName("PK__Externo__CF4B12E03BA2BD65");

            entity.Property(e => e.IdExterno).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdExternoNavigation).WithOne(p => p.Externo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Externo__ID_Exte__267ABA7A");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__886D5DEECDF1CD75");

            entity.Property(e => e.IdGrupo).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdEdicaoNavigation).WithMany(p => p.Grupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__ID_Edicao__33D4B598");

            entity.HasOne(d => d.IdGrupoNavigation).WithOne(p => p.Grupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__ID_Grupo__34C8D9D1");
        });

        modelBuilder.Entity<Interno>(entity =>
        {
            entity.HasKey(e => e.IdInterno).HasName("PK__Interno__EEA2B8265207726E");

            entity.Property(e => e.IdInterno).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdInternoNavigation).WithOne(p => p.Interno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Interno__ID_Inte__29572725");
        });

        modelBuilder.Entity<Orientador>(entity =>
        {
            entity.HasKey(e => e.IdOrientador).HasName("PK__Orientad__C15B3CC7AA06E5C6");
        });

        modelBuilder.Entity<Orientum>(entity =>
        {
            entity.HasKey(e => new { e.IdOrientador, e.IdProjeto }).HasName("PK__Orienta__EC6A0D1CBD0E5614");

            entity.HasOne(d => d.IdOrientadorNavigation).WithMany(p => p.Orienta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orienta__ID_Orie__3B75D760");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Orienta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orienta__ID_Proj__3C69FB99");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.IdProjeto).HasName("PK__Projeto__D3131DB335E3DA52");

            entity.HasOne(d => d.IdEdicaoNavigation).WithMany(p => p.Projetos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projeto__ID_Edic__300424B4");

            entity.HasOne(d => d.IdOrientadorNavigation).WithMany(p => p.Projetos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projeto__ID_Orie__30F848ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public DbSet<SitemaProjeto.Models.Notas>? Notas { get; set; }
}
