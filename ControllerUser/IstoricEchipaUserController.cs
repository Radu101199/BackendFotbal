using AspNetCoreApp1.DataContext;
using AspNetCoreApp1.Models.DTO.IstoricEchipe;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IstoricEchipeUserController : Controller
    {
        private readonly TeamAppDb _teamAppDb;
        private readonly IMapper _mapper;

        public IstoricEchipeUserController(TeamAppDb teamAppDb, IMapper mapper)
        {
            _teamAppDb = teamAppDb;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAllIstoricEchipe()
        {
            var istoricEchipe = _teamAppDb.IstoricEchipe.Include(j=> j.Jucator).Include(e=>e.Echipa).ToList();
            var istoricEchipeToReturn = _mapper.Map<List<IstoricEchipeViewDto>>(istoricEchipe);
            return Ok(istoricEchipeToReturn);
        }
        
        [HttpGet]
        [Route("IstoricEchipa")]
        public IActionResult GetIstoricEchipa(int? idIstoricEchipa)
        {
            if (idIstoricEchipa == 0)
            {
                return BadRequest("Nu exista un istoric de echipa cu acest id!");
            }
            {
                var existingIstoricEchipe = _teamAppDb.IstoricEchipe
                    .Where(t => t.IdIstoricEchipa.Equals(idIstoricEchipa)).Include(j=> j.Jucator).Include(e=>e.Echipa).SingleOrDefault();
                var istoricEchipaToReturn = _mapper.Map<IstoricEchipeViewDto>(existingIstoricEchipe);
                if (istoricEchipaToReturn == null)
                    return NotFound("Nu exista niciun istoric de echipa cu acest Id!");
        
                return Ok(existingIstoricEchipe);
            }
        
            return BadRequest();
        }
    }
}