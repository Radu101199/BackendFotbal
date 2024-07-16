using System.ComponentModel.DataAnnotations;
namespace AspNetCoreApp1.Models.DTO.Stadioane
{
    public class StadionEditDto
    {
        public required int IdStadion { get; set; }

        public required int IdLocatieNou { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string NumeNou { get; set; }
        [Range(1000, 500000,  ErrorMessage = "Capacitate trebuie sa fie intre 1000 si 500000")]
        public required int CapacitateNoua { get; set; }
    }
}