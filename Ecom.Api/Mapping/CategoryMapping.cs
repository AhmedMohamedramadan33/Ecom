using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;

namespace Ecom.Api.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        }
    }
}
