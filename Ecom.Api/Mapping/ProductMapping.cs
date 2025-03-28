using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;

namespace Ecom.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().
                ForMember(desc => desc.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).
                 ForMember(desc => desc.Photos, opt => opt.MapFrom(src => src.Photos.SelectMany(x => new List<string>() { x.ImageName }))).ReverseMap();

            CreateMap<Photo, PhotoDto>().
             ReverseMap();

            CreateMap<AddProductDto, Product>().
                ForMember(desc => desc.Photos, op => op.Ignore());

            CreateMap<UpdateProductDto, Product>().
               ForMember(desc => desc.Photos, op => op.Ignore());
        }
    }
}
