using AutoMapper;
using WebApi.Helpers;

public class MappingProfiles : Profile
{

    public MappingProfiles(IConfiguration config)
    {
        CreateMap<Product, ProductDTO>()
        .ForMember(P => P.BrandName, options => options.MapFrom(P => P.Brand != null ? P.Brand.Name : "Test"))
        .ForMember(P => P.TypeName, options => options.MapFrom(P => P.Type != null ? P.Type.Name : "Test"))
        .ForMember(P => P.PictureUrl, options => options.MapFrom(new ImageUrlProvider(config)));
    }
}