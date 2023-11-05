using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Projeto")]
public partial class Projeto
{
    [Key]
    [Column("ID_Projeto")]
    public int IdProjeto { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [Column("Num_Alunos")]
    public int NumAlunos { get; set; }

    [Column("Area_Investigacao")]
    [StringLength(50)]
    [Unicode(false)]
    public string AreaInvestigacao { get; set; } = null!;

    [Column("Centro_Investigacao")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CentroInvestigacao { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string Apresentacao { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Objetivos { get; set; } = null!;

    [Column("ID_Orientador")]
    public int IdOrientador { get; set; }

    [Column("ID_Edicao")]
    public int IdEdicao { get; set; }

    [InverseProperty("IdProjetoNavigation")]

    [Column("Coorientador_Interno")]
    [StringLength(500)]
    [Unicode(false)]
    public string? CoorientadorInterno { get; set; }

    [Column("Coorientador_Externo")]
    [StringLength(500)]
    [Unicode(false)]
    public string? CoorientadorExterno{ get; set; }
    public virtual ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();

    [ForeignKey("IdEdicao")]
    [InverseProperty("Projetos")]
    public virtual Edicao IdEdicaoNavigation { get; set; } = null!;

    [ForeignKey("IdOrientador")]
    [InverseProperty("Projetos")]
    public virtual Orientador IdOrientadorNavigation { get; set; } = null!;

    [InverseProperty("IdProjetoNavigation")]
    public virtual ICollection<Orientum> Orienta { get; set; } = new List<Orientum>();
}
