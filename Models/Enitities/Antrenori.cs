using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("Antrenori")]
public class Antrenori
{
    [Key]
    public int IdAntrenor { get; set; }
    public DateTime DataNasterii { get; set; }
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public int IdTara { get; set; }
    public string PozaProfil { get; set; }
    
    [ForeignKey("IdTara")]
    public Tari? Tara { get; set; }
}