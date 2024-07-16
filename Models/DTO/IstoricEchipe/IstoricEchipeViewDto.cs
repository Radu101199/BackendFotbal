using System.Runtime.InteropServices.JavaScript;

namespace AspNetCoreApp1.Models.DTO.IstoricEchipe;

public class IstoricEchipeViewDto
{
    public required int IdIstoricEchipa { get; set; }
    
    public string? Jucator { get; set; }
    
    public string? Echipa { get; set; }
    
    public DateTime DataInceput { get; set; }
    
    public DateTime DataFinal { get; set; }
}