using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

//[PrimaryKey("IdOrientador", "IdProjeto")]
public partial class Orientum
{
    [Key]
    [Column("ID_Orientador")]
    public int IdOrientador { get; set; }

    [Key]
    [Column("ID_Projeto")]
    public int IdProjeto { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Tipo { get; set; } = null!;

    [ForeignKey("IdOrientador")]
    [InverseProperty("Orienta")]
    public virtual Orientador IdOrientadorNavigation { get; set; } = null!;

    [ForeignKey("IdProjeto")]
    [InverseProperty("Orienta")]
    public virtual Projeto IdProjetoNavigation { get; set; } = null!;
}
