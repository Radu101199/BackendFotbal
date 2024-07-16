using System.Runtime.InteropServices.JavaScript;
namespace AspNetCoreApp1.Models.DTO.IstoricEchipe;
using System.ComponentModel.DataAnnotations;
public class IstoricEchipeDto
{
    public required int IdEchipa { get; set; }

    public required int IdJucator { get; set; }
    [DataType(DataType.Date)] 
    public required DateTime DataInceput { get; set; }
    [DataType(DataType.Date)] 
    public required DateTime DataSfarsit { get; set; }
}