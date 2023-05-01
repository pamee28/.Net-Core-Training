using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
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
        //POST: https/localhost:portnumber/api.walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]AddWalkRequiestDto addWalkRequiestDto)
        {
     

                //Map DTO to Domain Model
                var walDomainModel = mapper.Map<Walk>(addWalkRequiestDto);

                await walkRepository.CreateAsync(walDomainModel);


                //Map domin model to DTO
                return Ok(mapper.Map<WalkDto>(walDomainModel));

       
        }


        //GET Walk
        //get: /api/walks?filterOn=Name&filterQuery=Track&sortBY=Name&IsAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string?filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery]int pageNumber=1, [FromQuery] int pageSize =1000)
        {
         
                var walksDomaninModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                    pageNumber, pageSize);

            //create an exception
           // throw new Exception("This is a new exception");

                //Map domain model to dto
                return Ok(mapper.Map<List<WalkDto>>(walksDomaninModel));
            
           
        }

        //GET Walk by id
        //GET : /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        //Update Walk By Id
        //PUT :/api/walks/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute]Guid id, UpdateWalkRequestDto updateWalkRequestDto )
        {
       


                //Mao DTO to domain Model

                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //Map domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
    
        }

        //Delete Walk by id
        //DELETE:/ api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deletedWalkDomainMondel= await walkRepository.DeleteAsync(id);
            if(deletedWalkDomainMondel == null)
            {
                return NotFound();
            }

            //MaP domain model to DTO

            return Ok(mapper.Map<WalkDto>(deletedWalkDomainMondel));
        }

    }
}
