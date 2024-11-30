using AutoMapper;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO,Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();
        }
    }
}
