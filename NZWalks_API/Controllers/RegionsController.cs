using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: api/Regions It is to return all the regions in the database to this endpoint
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get all the regions from the database - Domain Model
            //We use the ToListAsync method to get all the regions from the database in case the database is large
            //and depending on the size of the database it could take a while to get the data so we use the async method and 
            //the await keyword

            //We are now using the repository pattern to get the regions from the database
            //so we will comment out the below line and use the interface method to get the regions

            /*var regionsDomainModel =  await dbContext.Regions.ToListAsync();*/

            var regionsDomainModel = await regionRepository.GetAllAsync();

            /*
            //Map the domain model to the DTO model
            var regionsDto = new List<RegionDTO>();

            foreach (var region in regionsDomainModel)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageurl = region.RegionImageurl
                });

            }*/
            //map domain models to dto models
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomainModel);


            //Return dto model to the client
            return Ok(regionsDto);
        }


        //GetSingleRegion method is to return a single region based on the id passed in the URL
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)//since we have the ids as guid we will get them as guid
        {
            //the find method can only be used with the ID property
            //Get regionDomainmodel from database

            /*var regionDomainModel = await dbContext.Regions.FindAsync(id);*/

            var regionDomainModel = await regionRepository.GetByIdAsync(id);

            //the first or default method can be used to find other camps that arent id
            //var region = dbContext.Regions.FirstOrDefault(x => x.Code == "AKl");



            /*
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //Map the domain model to the DTO model
                var regionDto = new RegionDTO()
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    Code = regionDomainModel.Code,
                    RegionImageurl = regionDomainModel.RegionImageurl
                };*/

            var regionDto =   mapper.Map<RegionDTO>(regionDomainModel);


            return Ok(regionDto);
        }


        //Post method to add a new region to the database
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert the DTO model to the domain model
            /* var regionDomainModel = new Region()
             {
                 Name = addRegionRequestDto.Name,
                 Code = addRegionRequestDto.Code,
                 RegionImageurl = addRegionRequestDto.RegionImageurl
             };*/
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //Ude Domain model to add the new region to the database
            // await dbContext.Regions.AddAsync(regionDomainModel);
            // await dbContext.SaveChangesAsync();

            regionDomainModel =  await regionRepository.CreateAsync(regionDomainModel);

            //since we cant send the domain model to the client we will convert it to the DTO model
            /* var regionDomainModel= new RegionDTO()
             {
                 Id = regionDomainModel.Id,
                 Name = regionDomainModel.Name,
                 Code = regionDomainModel.Code,
                 RegionImageurl = regionDomainModel.RegionImageurl
             };*/

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }


        //Put method to update a region in the database
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto regionRequestDto)
        {
            //check if region exists

            //remove this check because it is dealt with in the repository
            /*   var regiondomainmodel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

               if (regiondomainmodel == null)
               {
                   return NotFound();
               }*/
            //Map DTO to domain model
            /* var regiondomainmodel = new Region()
             {

                 Code = regionRequestDto.Code,
                 Name = regionRequestDto.Name,
                 RegionImageurl = regionRequestDto.RegionImageurl
             };*/
            var regiondomainmodel = mapper.Map<Region>(regionRequestDto);


            regiondomainmodel =  await regionRepository.UpdateAsync(id,regiondomainmodel);
            if (regiondomainmodel == null)
            {
                return NotFound();
            }

            /*Map DTO to domain model old
            regiondomainmodel.Code = regionRequestDto.Code;
            regiondomainmodel.Name = regionRequestDto.Name;
            regiondomainmodel.RegionImageurl = regionRequestDto.RegionImageurl;*/

           // await dbContext.SaveChangesAsync();

            //cconvevrt domain model to DTO model
          /*  var regionDto = new RegionDTO()
            {
                Id = regiondomainmodel.Id,
                Name = regiondomainmodel.Name,
                Code = regiondomainmodel.Code,
                RegionImageurl = regiondomainmodel.RegionImageurl
            };*/

            var regionDto = mapper.Map<RegionDTO>(regiondomainmodel);

            return Ok(regionDto);
        }

        //Delete method to delete a region from the database
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var regionDomainModel =  await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
           // dbContext.Regions.Remove(regionDomainModel);
          // await dbContext.SaveChangesAsync();


            return Ok();
        }
    }

}
