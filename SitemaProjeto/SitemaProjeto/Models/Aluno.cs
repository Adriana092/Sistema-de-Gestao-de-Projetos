using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Aluno")]
public partial class Aluno
{
    [Key]
    [Column("ID_Aluno")]
    public int IdAluno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Curso { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("Numero_Mec")]
    public int NumeroMec { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [InverseProperty("IdGrupoNavigation")]
    public virtual Grupo? Grupo { get; set; }

    [ForeignKey("IdAluno")]
    [InverseProperty("IdAlunos")]
    public virtual ICollection<Grupo> IdGrupos { get; set; } = new List<Grupo>();
}
