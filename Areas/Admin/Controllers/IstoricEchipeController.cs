using Microsoft.AspNetCore.Mvc;
using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.DTO.IstoricEchipe;
using AspNetCoreApp1.Models.Enitities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp1.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class IstoricEchipeController : Controller
    {
        
        private readonly TeamAppDb _teamAppDb;
        private readonly IMapper _mapper;

        public IstoricEchipeController(TeamAppDb teamAppDb, IMapper mapper)
        {
            _teamAppDb = teamAppDb;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddIstoricEhipa([FromBody] IstoricEchipeDto istoricEchipeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingEchipa = _teamAppDb.Echipe.Where(t => t.IdEchipa.Equals(istoricEchipeDto.IdEchipa))
                        .SingleOrDefault();
                    if (existingEchipa == null)
                        return NotFound("Nu exista echipa cu acest Id");
                    var existingJucator = _teamAppDb.Jucatori.Where(t => t.IdJucator.Equals(istoricEchipeDto.IdJucator))
                        .SingleOrDefault();
                    if (existingJucator == null)
                        return NotFound("Nu exista jucator cu acest Id");
                    if (istoricEchipeDto.DataInceput >= istoricEchipeDto.DataSfarsit)
                        return BadRequest("Data sfarsit nu poate fi mai mare sau egala decat data inceput!");
                    
                    var istoricEchipa = _mapper.Map<IstoricEchipe>(istoricEchipeDto);
                    if (istoricEchipa != null)
                    {
                        var istoricEchipaToReturn = _teamAppDb.IstoricEchipe
                            .Where(i => i.IdIstoricEchipa == istoricEchipa.IdIstoricEchipa)
                            .Include(e => e.Echipa.Locatie.Tara)
                            .Include(e => e.Echipa.Antrenor.Tara)
                            .Include(e => e.Echipa.Campionat)
                            .Include(e=> e.Echipa!.Stadion.Locatie.Tara)
                            .Include(j=> j.Jucator.Echipa.Locatie.Tara)
                            .Include(j=> j.Jucator.Echipa.Antrenor.Tara)
                            .Include(j=> j.Jucator.Echipa.Campionat)
                            .Include(j=> j.Jucator.Echipa.Stadion.Locatie.Tara)
                            .Include(j=> j.Jucator.Pozitie)
                            .Include(j=> j.Jucator.Tara);
                        
                        _teamAppDb.IstoricEchipe.Add(istoricEchipa);
                        _teamAppDb.SaveChanges();
                        
                        return Ok(istoricEchipaToReturn);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("FOREIGN"))
                {
                    return BadRequest("Foreign key");
                }
                else if (e.ToString().Contains("unique index"))
                {
                    return BadRequest("Nu poti adauga 2 IstoricEchipa despre acelasi jucator la aceeasi echipa in acelasi timp!");
                }
                else
                {
                    return BadRequest(e.ToString());
                }
            }
            return BadRequest();
        }

        
        [HttpGet]
        public IActionResult GetIstoricEchipe(int idIstoricEchipa)
        {
            if (idIstoricEchipa == 0)
            {
                var istoricEchipe = _teamAppDb.IstoricEchipe
                    .Include(e => e.Echipa)
                    .Include(j=> j.Jucator)
                    .ToList();
                var istoricEchipeToReturn = _mapper.Map<List<IstoricEchipeViewDto>>(istoricEchipe);
                return Ok(istoricEchipeToReturn);
            }
            else
            {
                var existingIstoricEchipe = _teamAppDb.IstoricEchipe
                    .Where(t => t.IdIstoricEchipa.Equals(idIstoricEchipa)).Include(j=> j.Jucator).Include(e=>e.Echipa).SingleOrDefault();
                var istoricEchipaToReturn = _mapper.Map<IstoricEchipeViewDto>(existingIstoricEchipe);
                if (istoricEchipaToReturn == null)
                    return NotFound("Nu exista niciun istoric de echipa cu acest Id!");
        
                return Ok(istoricEchipaToReturn);
            }
        }

        [HttpPut]
        public IActionResult EditIstoricEchipa([FromBody] IstoricEchipaEditDto istoricEchipaEditDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    var existingIstoricEchipa = _teamAppDb.IstoricEchipe.Where(i => i.IdIstoricEchipa == istoricEchipaEditDto.IdIstoricEchipa)
                        .SingleOrDefault();
                    if(istoricEchipaEditDto.DataInceputNoua >= istoricEchipaEditDto.DataSfarsitNoua)
                        return BadRequest("Data sfarsit nu poate fi mai mare sau egala decat data inceput!");
                    if (existingIstoricEchipa != null)
                    {
                        _mapper.Map(istoricEchipaEditDto,existingIstoricEchipa);
                        _teamAppDb.SaveChanges();
                        var existingIstoricEchipaToReturn = _teamAppDb.IstoricEchipe.Where(i=>i.IdIstoricEchipa == existingIstoricEchipa.IdIstoricEchipa)
                            .Include(e => e.Echipa.Locatie.Tara)
                            .Include(e => e.Echipa.Antrenor.Tara)
                            .Include(e => e.Echipa.Campionat)
                            .Include(e=> e.Echipa.Stadion.Locatie.Tara)
                            .Include(j=> j.Jucator.Echipa.Locatie.Tara)
                            .Include(j=> j.Jucator.Echipa.Antrenor.Tara)
                            .Include(j=> j.Jucator.Echipa.Campionat)
                            .Include(j=> j.Jucator.Echipa.Stadion.Locatie.Tara)
                            .Include(j=> j.Jucator.Pozitie)
                            .Include(j=> j.Jucator.Tara);
                        return Ok(existingIstoricEchipaToReturn);
                    }
                    else
                    {
                        return NotFound(
                            "Nu exista niciun istoric echipa cu acest id");
                    }
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("unique index"))
                        return BadRequest("Nu pot exista 2 istoricuri de echipa cu aceeasi echipa si jucator in aceesi perioada!");
                    if (e.ToString().Contains("FOREIGN KEY") && e.ToString().Contains("column 'IdEchipa'"))
                        return BadRequest("Nu exista nicio echipa cu acest id!");
                    if (e.ToString().Contains("FOREIGN KEY") && e.ToString().Contains("column 'IdJucator'"))
                        return BadRequest("Nu exista niciun jucator cu acest id!");
                    return BadRequest(e.ToString());
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteIstoricEchipa(int idIstoricEchipa)
        {
            if (idIstoricEchipa == 0)
                return BadRequest("Introdu o valoare diferita de null!");
            var existingIstoricEchipa = _teamAppDb.IstoricEchipe.Where(i =>i.IdIstoricEchipa == idIstoricEchipa)
                .SingleOrDefault();
            if (existingIstoricEchipa == null)
                return NotFound(
                    "Nu exista niciun istoric echipa cu acest id");
            _teamAppDb.IstoricEchipe.Remove(existingIstoricEchipa);
            _teamAppDb.SaveChanges();
            return Ok();
        }
    }
}