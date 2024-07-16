using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.DTO.Tari;
using AspNetCoreApp1.Models.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp1.Areas.Admin.Controllers
{
[Route("api/admin/[controller]")]
[ApiController]
    public class TariController : ControllerBase
    {
        private readonly TeamAppDb _teamAppDb;

        public TariController(TeamAppDb db)
        {
            this._teamAppDb = db;
        }

        [HttpGet]
        public IActionResult GetTari(int id)
        {
            if (id != 0)
            {
                var existingTara = _teamAppDb.Tari.Where(t => t.IdTara.Equals(id))
                    .SingleOrDefault();
                if (existingTara == null)
                    return NotFound("Nu exista nicio inregistrare pentru acest id!");
            
                return Ok(existingTara);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddTari([FromBody] TaraDto taraDto)
        {
            if (ModelState.IsValid)
            {
                Tari tara = new Tari { Denumire = taraDto.Denumire };
                try
                {
                    _teamAppDb.Tari.Add(tara);
                    _teamAppDb.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest("Exista deja aceasta inregistrare");
                }
            }

            return BadRequest();
        }
    }
}