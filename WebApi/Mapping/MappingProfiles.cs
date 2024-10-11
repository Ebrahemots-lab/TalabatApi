using AutoMapper;
using WebApi.Helpers;

public class MappingProfiles : Profile
{

    public MappingProfiles(IConfiguration config)
    {
        CreateMap<Product, ProductDTO>()
        .ForMember(P => P.BrandName, options => options.MapFrom(P => P.Brand.Name))
        .ForMember(P => P.TypeName, options => options.MapFrom(P => P.Type.Name))
        .ForMember(P => P.PictureUrl, options => options.MapFrom(new ImageUrlProvider(config)));
    }
}