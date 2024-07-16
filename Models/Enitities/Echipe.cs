using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("Echipe")]
public class Echipe
{
    [Key]
    public int IdEchipa { get; set; }
    public string Nume { get; set; }
    public string Emblema { get; set; }
    public int IdLocatie { get; set; }
    public int IdAntrenor { get; set; }
    public int IdCampionat { get; set; }
    public int IdStadion { get; set; }
    public decimal ValoareEchipa { get; set; }
    public DateTime DataInfiintare { get; set; }
    
    [ForeignKey("IdLocatie")]
    public Locatii? Locatie { get; set; }
    
    [ForeignKey("IdAntrenor")]
    public Antrenori? Antrenor { get; set; }
    
    [ForeignKey("IdCampionat")]
    public Campionate? Campionat { get; set; }
    
    [ForeignKey("IdStadion")]
    public Stadioane? Stadion { get; set; }
}