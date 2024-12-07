using AutoMapper;

namespace PortfolioService.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Post, DTOs.Post.PostDto>().ReverseMap();
        CreateMap<Models.Post, DTOs.Post.PostListingDto>().ReverseMap();
        CreateMap<Models.Post, DTOs.Post.PostCreateDto>().ReverseMap();
        CreateMap<Models.Post, DTOs.Post.PostUpdateDto>().ReverseMap();

        CreateMap<Models.FeaturedItem, DTOs.Featured.FeaturedItemDto>().ReverseMap();
    }
}