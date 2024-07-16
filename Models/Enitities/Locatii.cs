using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApp1.Models.Enitities;

[Table("Locatii")]
public class Locatii
{
    [Key]
    public int IdLocatie { get; set; }
    public int IdTara { get; set; }
    public required string Oras { get; set; }
    
    [ForeignKey("IdTara")]
    public Tari? Tara { get; set; }
    
}