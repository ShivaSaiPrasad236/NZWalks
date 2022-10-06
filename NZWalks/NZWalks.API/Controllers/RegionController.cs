using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using NZWalks.API.Models.Domains;
using System.Security.Cryptography.X509Certificates;
using NZWalks.API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper )
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    regionsDTO.Add(new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Area = region.Area
            //    });
            //});
            var result = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
            
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // crete region
            var region = new Models.Domains.Region()
            {
                // dto request to domain
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Area = addRegionRequest.Area,
                Populaiton = addRegionRequest.Populaiton
            };
            // add to repository

            region = await regionRepository.AddAsync(region);

            // convert to dto

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Populaiton = region.Populaiton
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { Id = regionDTO.Id }, regionDTO);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id) 
        {
            // find in database

            var region = await regionRepository.DeleteAsync(id);

            // if region not found
            if (region == null)
            {
                return NotFound();
            }
            // convert to database
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Populaiton = region.Populaiton,
                Area = region.Area
            };
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegion)
        {
            //get details from dto

            var region = new Models.Domains.Region()
            {
                Name = updateRegion.Name,
                Code = updateRegion.Code,
                Lat = updateRegion.Lat,
                Long = updateRegion.Long,
                Populaiton = updateRegion.Populaiton,
                Area = updateRegion.Area
            };

            //find in database
            region = await regionRepository.UpdateAsync(id, region);

            //return null if not found
            if (region == null)
            {
                return NotFound();
            }
            // domain region to dto
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Populaiton = region.Populaiton,
                Area = region.Area
            };


            //return result;
            return Ok(regionDTO);


        }


    }
}
