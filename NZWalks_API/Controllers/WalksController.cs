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

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE WALK 
        //POST: /api/walks
        [HttpPost] //Since we are getting data from the client we must accept the user through DTO, so we create a dto for walks client

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //Map the DTO from Swagger to the Walk Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

            await walkRepository.CreateAsync(walkDomainModel);


            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }


        //Get Walks
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //mappping the domain model to the DTO

            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        //Get Walk by ID 
        //GET: /api/walks/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkdomainmodel = await walkRepository.GetByIdAsync(id);

            if (walkdomainmodel == null)
            {
                return NotFound();
            }
            //mapping the domain model to the DTO
            return Ok(mapper.Map<WalkDTO>(walkdomainmodel));
        }

        //Update Walk By Id
        //PUT: /api/walks/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Map DTO To Domain Model

            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Mapping the domain model to the DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        //Delete Walk By Id
        //DELETE: /api/walks/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var deletedwalkDomainModel = await walkRepository.DeleteAsync(id);

            if (deletedwalkDomainModel == null)
            {
                return NotFound();
            }

            //Mapping the domain model to the DTO
            return Ok(mapper.Map<WalkDTO>(deletedwalkDomainModel));

        }

    }

    }
