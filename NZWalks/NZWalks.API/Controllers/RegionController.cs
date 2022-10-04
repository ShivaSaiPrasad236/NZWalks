using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using NZWalks.API.Models.Domains;
using System.Security.Cryptography.X509Certificates;
using NZWalks.API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper;

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
            var regions = await regionRepository.GetAllRegionsAsync();
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
        
    }
}
