using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Interno")]
public partial class Interno
{
    [Key]
    [Column("ID_Interno")]
    public int IdInterno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [ForeignKey("IdInterno")]
    [InverseProperty("Interno")]
    public virtual Orientador IdInternoNavigation { get; set; } = null!;
}
