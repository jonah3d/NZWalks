using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE WALK 
        //POST: /api/walks
        [HttpPost] //Since we are getting data from the client we must accept the user through DTO, so we create a dto for walks client
        
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO )
        {
            //Map the DTO from Swagger to the Walk Domain Model
            var walkDomainModel =   mapper.Map<Walk>(addWalkRequestDTO);
 
            await walkRepository.CreateAsync(walkDomainModel);


            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
    }
}
