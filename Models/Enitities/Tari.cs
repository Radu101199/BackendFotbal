using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("Tari")]
public class Tari
{
    [Key]
    public int IdTara { get; set; }
    public required string Denumire { get; set; }
}