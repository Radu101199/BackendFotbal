using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("IstoricEchipe")]
public class IstoricEchipe
{
    [Key]
    public int IdIstoricEchipa { get; set; }
    public int IdEchipa { get; set; }
    public int IdJucator { get; set; }
     
    public DateTime DataInceput { get; set; }
   
    public DateTime DataFinal { get; set; }

    [ForeignKey("IdEchipa")]
    public Echipe? Echipa { get; set; }
    
    [ForeignKey("IdJucator")]
    public Jucatori? Jucator { get; set; }
}