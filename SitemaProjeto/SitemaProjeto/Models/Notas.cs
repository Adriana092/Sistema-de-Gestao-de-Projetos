using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SitemaProjeto.Models;

[Table("Notas")]
public partial class Notas
{

    [Key]
    [Column("ID_Nota")]
    public int IdNota { get; set; }


    [Column("Numero_Mec")]
    public int Numero_Mec { get; set; }

    [Column("Num_Cadeiras")]
    public int Num_Cadeiras { get; set; }


    [Column("Media")]
    public float Media { get; set; }
}