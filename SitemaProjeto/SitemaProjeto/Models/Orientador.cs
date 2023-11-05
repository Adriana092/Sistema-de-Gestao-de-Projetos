using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Orientador")]
public partial class Orientador
{
    [Key]
    [Column("ID_Orientador")]
    public int IdOrientador { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Instituicao { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [InverseProperty("IdExternoNavigation")]
    public virtual Externo? Externo { get; set; }

    [InverseProperty("IdInternoNavigation")]
    public virtual Interno? Interno { get; set; }

    [InverseProperty("IdOrientadorNavigation")]
    public virtual ICollection<Orientum> Orienta { get; set; } = new List<Orientum>();

    [InverseProperty("IdOrientadorNavigation")]
    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();

    public List<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
}
