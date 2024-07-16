using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.DTO.Campionate;
using AspNetCoreApp1.Models.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp1.Areas.Admin.Controllers;

[Route("api/admin/[controller]")]
[ApiController]

public class CampionateController : ControllerBase
{

    private readonly TeamAppDb _teamAppDb;

    public CampionateController(TeamAppDb teamAppDb)
    {
        _teamAppDb = teamAppDb;
    }

    [HttpPost]
    public IActionResult AddCampionat([FromBody] CampionatDto campionatDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Campionate campionat = new Campionate
                {
                    Denumire = campionatDto.Denumire,
                };
                try
                {
                    _teamAppDb.Campionate.Add(campionat);
                    _teamAppDb.SaveChanges();
                    return Ok(campionat);
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("unique index"))
                    {
                         return BadRequest("Exista deja aceasta inregistrare");
                    }

                    return BadRequest(e.ToString());
                }
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            if (e.ToString().Contains("unique index"))
            {
                return BadRequest("Unique index");
            }
            return BadRequest(e.ToString());
        }
    }

    [HttpGet]
    public IActionResult GetCampionate(int idCampionat)
    {
        if (idCampionat == 0)
        {
            var campionate = _teamAppDb.Campionate.ToList();
            return Ok(campionate);
        }
        else
        {
            var existingCampioanat = _teamAppDb.Campionate.Where(t => t.IdCampionat.Equals(idCampionat))
                .SingleOrDefault();
            if (existingCampioanat == null)
                return NotFound("Nu exista nicio inregistrare pentru acest id!");
            
            return Ok(existingCampioanat);
        }
    }

    [HttpPut]
    public IActionResult EditCampionat([FromBody] CampionatEditDto campionatEditDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var existingCampioanat = _teamAppDb.Campionate
                    .Where(t => t.IdCampionat.Equals(campionatEditDto.IdCampionat))
                    .SingleOrDefault();
                if (existingCampioanat != null)
                {
                    existingCampioanat.Denumire = campionatEditDto.DenumireNoua;
                    _teamAppDb.SaveChanges();
                    return Ok(existingCampioanat);
                }
                else
                {
                    return NotFound("Nu exista niciun campionat cu acest id");
                }
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("unique index"))
                    return BadRequest("Nu pot exista 2 campionate cu aceeasi denumire!");
                return BadRequest(e.ToString());
            }
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult DeleteCampionat(int idCampionat)
    {
        try
        {
            if (idCampionat == 0)
                return BadRequest("Introdu o valoare diferita de null!");
            var existingCampioanat = _teamAppDb.Campionate.Where(t => t.IdCampionat.Equals(idCampionat))
                .SingleOrDefault();
            if (existingCampioanat == null)
                return BadRequest("Nu exista niciun campionat cu acest id!");

            _teamAppDb.Campionate.Remove(existingCampioanat);
            _teamAppDb.SaveChanges();
            return Ok();
        }
        catch (Exception e)
        {
            if (e.ToString().Contains("REFERENCE constraint"))
            {
                return BadRequest("Nu poti sterge un campionat in care inca joaca echipe!");
            }
            return BadRequest(e.ToString());   
        }
    }
    
}