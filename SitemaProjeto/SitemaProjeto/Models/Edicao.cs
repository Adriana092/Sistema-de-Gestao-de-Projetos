using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Edicao")]
public partial class Edicao
{
    [Key]
    [Column("ID_Edicao")]
    public int IdEdicao { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    [Column("Valido_de")]
    public int ValidoDe { get; set; }

    [Column("Valido_ate")]
    public int ValidoAte { get; set; }

    [InverseProperty("IdEdicaoNavigation")]
    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    [InverseProperty("IdEdicaoNavigation")]
    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();
}
