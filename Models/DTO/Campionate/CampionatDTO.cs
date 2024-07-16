using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp1.Models.DTO.Campionate;

public class CampionatDto
{
    [Required(AllowEmptyStrings = false)]
    public required String Denumire { get; set; }
}