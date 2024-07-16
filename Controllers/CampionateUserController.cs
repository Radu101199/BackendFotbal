using AspNetCoreApp1.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp1.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CampionateUserController : ControllerBase
{

    private readonly TeamAppDb _teamAppDb;

    public CampionateUserController(TeamAppDb teamAppDb)
    {
        _teamAppDb = teamAppDb;
    }
    
    [HttpGet]
    public IActionResult GetCampionat(int idCampionat)
    {
        if (idCampionat == 0)
        {
            return BadRequest("Nu exista un campionat cu acest id!");
        }
        var existingCampioanat = _teamAppDb.Campionate.Where(t => t.IdCampionat.Equals(idCampionat))
            .SingleOrDefault();
        if (existingCampioanat == null)
            return NotFound("Nu exista nicio inregistrare pentru acest id!");
            
        return Ok(existingCampioanat);
    }
    
    [HttpGet]
    [Route("Campionat")]
    public IActionResult GetAllCampionate()
    {
        var campionate = _teamAppDb.Campionate.ToList();
        return Ok(campionate);
    }
}