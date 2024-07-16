using Backend.DTO.Locatie;
using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp1.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LocatiiController : ControllerBase
    {
        private readonly TeamAppDb _teamAppDb;

        public LocatiiController(TeamAppDb teamAppDb)
        {
            _teamAppDb = teamAppDb;
        }

        [HttpPost]
        public IActionResult AddLocatie([FromBody] LocatieDto locatieDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingTara = _teamAppDb.Tari.Where(t => t.Denumire.Equals(locatieDto.NumeTara)).SingleOrDefault();
                    if (existingTara == null)
                        return BadRequest("Nu exista nicio inregistrare pentru aceasta tara!");
                    int IdTara = existingTara.IdTara;
                    Locatii locatie = new Locatii
                    {
                        IdTara = IdTara,
                        Oras = locatieDto.Oras,
                    };
                    _teamAppDb.Locatii.Add(locatie);
                    _teamAppDb.SaveChanges();
                    return Ok(locatie);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("FOREIGN"))
                {
                    return BadRequest("Foreign key");
                }
                else if (ex.ToString().Contains("unique index"))
                {
                    return BadRequest("Unique index");
                }
                else
                {
                    return BadRequest(ex.ToString());
                }
            }
        }

        [HttpGet]
        public IActionResult GetLocatii(string? oras, string? numeTara)
        {
            if (oras == null && numeTara == null)
            {
                var locatii = _teamAppDb.Locatii.ToList();
                return Ok(locatii);
            }
            else if (oras != null && numeTara != null)
            {
                var existingTara = _teamAppDb.Tari.Where(t => t.Denumire == numeTara).SingleOrDefault();
                if (existingTara == null)
                    return BadRequest("Nu exista nicio inregistrare cu o tara cu acest nume!");
                int IdTara = existingTara.IdTara;

                var existingLocatie = _teamAppDb.Locatii.Where(l => l.Oras == oras && l.IdTara == IdTara).SingleOrDefault();
                if (existingLocatie == null)
                    return BadRequest("Nu exista nicio inregistrare cu aceasta locatie!");

                return Ok(existingLocatie);
            }
            else if(oras == null && numeTara != null)
            {
                var existingTara = _teamAppDb.Tari.Where(t => t.Denumire == numeTara).SingleOrDefault();
                if (existingTara == null)
                    return BadRequest("Nu exista nicio inregistrare cu o tara cu acest nume!");
                int IdTara = existingTara.IdTara;

                var locatii = _teamAppDb.Locatii.Where(l => l.IdTara == IdTara).ToList();
                return Ok(locatii);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult EditLocatie([FromBody] LocatieEditDto locatieEditDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int IdTaraVeche = _teamAppDb.Tari.Where(t => t.Denumire == locatieEditDto.NumeTaraVeche).SingleOrDefault().IdTara;

                    var existingTara = _teamAppDb.Tari.Where(t => t.Denumire == locatieEditDto.NumeTaraNoua).SingleOrDefault();
                    if (existingTara == null)
                        return NotFound("Nu exista nicio inregistrare de tara cu acest nume!");
                    int IdTaraNoua = existingTara.IdTara;

                    var existingLocatie = _teamAppDb.Locatii.Where(l => l.IdTara == IdTaraVeche && l.Oras == locatieEditDto.OrasVeche).SingleOrDefault();
                    if (existingLocatie != null)
                    {
                        existingLocatie.IdTara = IdTaraNoua;
                        existingLocatie.Oras = locatieEditDto.OrasNou;
                        _teamAppDb.SaveChanges();
                        return Ok(existingLocatie);
                    }
                    else
                    {
                        return BadRequest("Nu exista nicio locatie in aceasta tara cu acest nume");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteLocatie(string numeTara, string oras)
        {
            int IdTara = _teamAppDb.Tari.Where(t => t.Denumire == numeTara).SingleOrDefault().IdTara;
            var existingLocatie = _teamAppDb.Locatii.Where(l => l.IdTara == IdTara && l.Oras == oras).SingleOrDefault();
            if (existingLocatie == null)
                return BadRequest("Nu exista nicio locatie in aceasta tara, in acest oras!");
            _teamAppDb.Locatii.Remove(existingLocatie);
            _teamAppDb.SaveChanges();
            return Ok();
        }
    }
}
