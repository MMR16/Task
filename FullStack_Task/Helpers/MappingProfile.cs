using AutoMapper;
using Core.Entities;
using FullStack_Task.Dtos;

namespace FullStack_Task.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(e => e.ImagePath, q => q.MapFrom<ProductUrlRssolver>()); 
            CreateMap<Product, ProductToReturnDto>().ForMember(e => e.ImagePath, q => q.MapFrom<ProductUrlRssolver>()).ReverseMap();
            

            CreateMap<Product, ProductToAddDto>();
            CreateMap<Product, ProductToAddDto>().ReverseMap();

            CreateMap<Product, ProductToEditDto>();
            CreateMap<Product, ProductToEditDto>().ReverseMap();
        }
    }
}
