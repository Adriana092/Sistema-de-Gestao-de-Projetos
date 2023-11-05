using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Externo")]
public partial class Externo
{
    [Key]
    [Column("ID_Externo")]
    public int IdExterno { get; set; }

    [ForeignKey("IdExterno")]
    [InverseProperty("Externo")]
    public virtual Orientador IdExternoNavigation { get; set; } = null!;
}
