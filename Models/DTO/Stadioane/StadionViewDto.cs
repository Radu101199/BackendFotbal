using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp1.Models.DTO.Stadioane;

public class StadionViewDto
{
    public int IdStadion { get; set; }
    
    public string Locatie { get; set; }
    public string? Nume { get; set; }
    public int? Capacitate { get; set; }
}