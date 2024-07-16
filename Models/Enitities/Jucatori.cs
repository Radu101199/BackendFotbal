using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;
[Table("Jucatori")]
public class Jucatori
{
    [Key]
    public int IdJucator { get; set; }
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public int IdEchipa { get; set; }
    public decimal Salariu { get; set; }
    public int IdPozitie { get; set; }
    public int IdTara { get; set; }
    public DateTime DataNasterii { get; set; }
    public string PozaProfil { get; set; }
    
    [ForeignKey("IdEchipa")]
    public Echipe? Echipa { get; set; }
    
    [ForeignKey("IdPozitie")]
    public Pozitii? Pozitie { get; set; }
    
    [ForeignKey("IdTara")]
    public Tari? Tara { get; set; }
}