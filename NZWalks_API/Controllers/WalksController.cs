﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.CustomActionFilters;
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
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
           
                //Map the DTO from Swagger to the Walk Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

                await walkRepository.CreateAsync(walkDomainModel);


                return Ok(mapper.Map<WalkDTO>(walkDomainModel));
         
        }


        //Get Walks
        //GET: /api/walks?filterOn=Name&filterQuery=
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize =1000
            )
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true,
                pageNumber,pageSize);

            //mappping the domain model to the DTO

            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        //Get Walk by ID 
        //GET: /api/walks/{id}
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
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
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
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
