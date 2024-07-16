namespace AspNetCoreApp1.Models.DTO.Stadioane;
using System.ComponentModel.DataAnnotations;
public class StadionDto
{
    public required int IdLocatie { get; set; }
    [Required(AllowEmptyStrings = false)]
    public required string Nume { get; set; }
    [Range(1000, 500000,  ErrorMessage = "Capacitate trebuie sa fie intre 1000 si 500000")]
    public required int Capacitate { get; set; }
}