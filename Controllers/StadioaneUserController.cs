using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.DTO.Stadioane;
using AspNetCoreApp1.Models.Enitities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadioaneUserController : ControllerBase
    {
        private readonly TeamAppDb _teamAppDb;
        private readonly IMapper _mapper;

        public StadioaneUserController(TeamAppDb teamAppDb, IMapper mapper)
        {
            _teamAppDb = teamAppDb;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStadioane()
        {
            var stadioane = _teamAppDb.Stadioane.Include(l => l.Locatie).ThenInclude(t => t!.Tara).ToList();
            var stadioaneToReturn = _mapper.Map<List<StadionViewDto>>(stadioane);
            return Ok(stadioaneToReturn);
        }
        
        [HttpGet]
        [Route("Stadion")]
        public IActionResult GetStadioane(int? idStadion)
        {
            if (idStadion == 0)
            {
                return BadRequest("Nu exista un stadion cu acest id!");
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
        
            return BadRequest();
        }
        
        
    }
}