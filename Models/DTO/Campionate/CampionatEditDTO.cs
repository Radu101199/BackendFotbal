using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp1.Models.DTO.Campionate;

public class CampionatEditDto
{
    public required int IdCampionat { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public required String DenumireNoua { get; set; }
}