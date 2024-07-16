using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("Pozitii")]
public class Pozitii
{
    [Key]
    public int IdPozitie { get; set; }
    public string Denumire { get; set; }
}