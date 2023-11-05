using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

//[PrimaryKey("IdGrupo", "IdProjeto")]
[Table("Candidatura")]
public partial class Candidatura
{
    public int? Seriacao { get; set; }

    public bool? Resultado { get; set; }

    [Key]
    [Column("ID_Grupo")]
    public int IdGrupo { get; set; }

    [Key]
    [Column("ID_projeto")]
    public int IdProjeto { get; set; }

    [Column("Ordem_Preferencia")]
    public int OrdemPreferencia { get; set; }

    [ForeignKey("IdGrupo")]
    [InverseProperty("Candidaturas")]
    public virtual Grupo IdGrupoNavigation { get; set; } = null!;

    [ForeignKey("IdProjeto")]
    [InverseProperty("Candidaturas")]
    public virtual Projeto IdProjetoNavigation { get; set; } = null!;
}
