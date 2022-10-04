using AutoMapper;
namespace NZWalks.API.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domains.Region, Models.DTO.Region>();
        }
    }
}
