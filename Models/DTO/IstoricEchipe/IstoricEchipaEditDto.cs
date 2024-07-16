namespace AspNetCoreApp1.Models.DTO.IstoricEchipe;

public class IstoricEchipaEditDto
{
    public required int IdIstoricEchipa { get; set; }
    public required int IdEchipaNoua { get; set; }
    public required int IdJucatorNou { get; set; }
    public required DateTime DataInceputNoua { get; set; }

    public required DateTime DataSfarsitNoua { get; set; }
}