using Microsoft.AspNetCore.Mvc;
using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.DTO.Stadioane;
using AspNetCoreApp1.Models.Enitities;
using AutoMapper;
using Backend.DTO.Locatie;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp1.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StadioaneController : ControllerBase
    {
        private readonly TeamAppDb _teamAppDb;
        private readonly IMapper _mapper;

        public StadioaneController(TeamAppDb teamAppDb, IMapper mapper)
        {
            _teamAppDb = teamAppDb;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddStadion([FromBody] StadionDto stadionDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingLocatie = _teamAppDb.Locatii.Where(t => t.IdLocatie.Equals(stadionDto.IdLocatie))
                        .SingleOrDefault();
                    if (existingLocatie == null)
                        return NotFound("Nu exista o locatie cu acest id");

                    var stadion = _mapper.Map<Stadioane>(stadionDto);
                    if (stadion != null)
                    {
                        var stadionToReturn = _teamAppDb.Stadioane.Where(s => s.IdStadion == stadion.IdStadion)
                            .Include(l => l.Locatie).ThenInclude(t => t!.Tara);
                        _teamAppDb.Stadioane.Add(stadion);
                        _teamAppDb.SaveChanges();
                        return Ok(stadionToReturn);
                    }
                    return BadRequest("Obiectul este null");
                }
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("FOREIGN"))
                {
                    return BadRequest("Foreign key");
                }
                if (e.ToString().Contains("unique index"))
                {
                    return BadRequest("Nu pot exista 2 stadioane cu acelasi nume in aceeasi locatie!");
                }
                return BadRequest(e.ToString());
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetStadioane(int? idStadion)
        {
            
            if (idStadion == 0)
            {
                var stadioane = _teamAppDb.Stadioane.Include(l => l.Locatie).ThenInclude(t => t!.Tara).ToList();
                var stadioaneToReturn = _mapper.Map<List<StadionViewDto>>(stadioane);
                return Ok(stadioaneToReturn);
            }
            else
            {
                var existingStadion = _teamAppDb.Stadioane.Where(t => t.IdStadion.Equals(idStadion))
                    .Include(l=>l.Locatie)
                    .ThenInclude(t => t!.Tara)
                    .SingleOrDefault();
                var stadionToReturn = _mapper.Map<StadionViewDto>(existingStadion);
                if (stadionToReturn == null)
                    return NotFound("Nu exista niciun stadion cu acest Id!");
            
                return Ok(stadionToReturn);
            }
        }

        [HttpPut]
        public IActionResult EditStadion([FromBody] StadionEditDto stadionEditDto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var existingStadion = _teamAppDb.Stadioane.Where(t => t.IdStadion == stadionEditDto.IdStadion)
                        .SingleOrDefault();
                    
                    if (existingStadion != null)
                    {
                        _mapper.Map(stadionEditDto,existingStadion);
                        _teamAppDb.SaveChanges();
                        var existingStadionToReturn = _teamAppDb.Stadioane.Where(t => t.IdStadion == existingStadion.IdStadion).Include(l => l.Locatie).ThenInclude(t=>t!.Tara);
                        return Ok(existingStadionToReturn);
                    }
                    else
                    {
                        return NotFound("Nu exista niciun stadion cu acest id");
                    }
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("unique index"))
                        return BadRequest("Nu pot exista 2 stadioane in aceeasi locatie cu aceeasi denumire!");
                    if (e.ToString().Contains("FOREIGN KEY"))
                        return BadRequest("Nu exista nicio locatie cu acest id!");
                    return BadRequest(e.ToString());
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteStadion(int idStadion)
        {
            try
            {
                if (idStadion == 0)
                    return BadRequest("Introdu o valoare diferita de null!");
                var existingStadion = _teamAppDb.Stadioane.Where(s =>
                    s.IdStadion == idStadion).SingleOrDefault();
                if (existingStadion == null)
                    return NotFound("Nu exista niciun stadion cu acest id!");
                _teamAppDb.Stadioane.Remove(existingStadion);
                _teamAppDb.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("REFERENCE constraint"))
                {
                    return BadRequest("Nu poti sterge un stadion pe care inca joaca o echipa!");
                }

                return BadRequest(e.ToString());
            }
        }
    }
}