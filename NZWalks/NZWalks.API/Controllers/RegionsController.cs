using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;

        }
            //GET ALL REGIONS
            // GET: https://localhost:postnumer/api/regions

       [HttpGet]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception");
                //  Get data from Databas - Domain MOdels
                var regionsDomain = await regionRepository.GetAllAsync();

                // Return DTOs
                logger.LogInformation($"Finished GeAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
           
          
        }


        //GET SINGLE REGION(Region  by id)
        //GET :https://localhost:postnumer/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task <IActionResult> GetById(Guid id) 
         {
            // var region = dbContext.Regions.Find(id); 
            //Get Region Domain Model from Database
            var regionDomain = await regionRepository.GetByIDAsync(id);

            if (regionDomain==null)
            {
                return NotFound();
            }

            
            //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        //POST to create a new region
        // POST: https://localhost:portnumer/api/regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
           

                //Map ot convert dto todomain Model

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                //use domain model to create region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);



                //Map domain model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            
           
        }

        //Update regon
        //PUT: https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
       // [Authorize (Roles = "Writer")]
      
           
        public async Task< IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
                //Map DTO To Domain Model

                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
          
        }

        //Delete Region
        //DELETE : https://localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize (Roles = "Writer")]
        public async Task< IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
