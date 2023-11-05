using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("SelecionarGrupo")]
public partial class Grupo
{
    [Key]
    [Column("ID_Grupo")]
    public int IdGrupo { get; set; }

    [Column("ID_Edicao")]
    public int IdEdicao { get; set; }

    [InverseProperty("IdGrupoNavigation")]
    public virtual ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();

    [ForeignKey("IdEdicao")]
    [InverseProperty("Grupos")]
    public virtual Edicao IdEdicaoNavigation { get; set; } = null!;

    [ForeignKey("IdGrupo")]
    [InverseProperty("Grupo")]
    public virtual Aluno IdGrupoNavigation { get; set; } = null!;

    [ForeignKey("IdGrupo")]
    [InverseProperty("IdGrupos")]
    public virtual ICollection<Aluno> IdAlunos { get; set; } = new List<Aluno>();

    [Column("Numero_Mec1")]
    public int NumeroMec1 { get; set; }

    [Column("Numero_Mec2")]
    public int? NumeroMec2 { get; set; }

    [Column("Numero_Mec3")]
    public int? NumeroMec3 { get; set; }

    [Column("Numero_Mec4")]
    public int? NumeroMec4 { get; set; }
}
