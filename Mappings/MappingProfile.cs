using AutoMapper;

namespace PortfolioService.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Post, DTOs.Post.PostDto>().ReverseMap();
        CreateMap<Models.Post, DTOs.Post.PostListingDto>().ReverseMap();
    }
}