using AutoMapper;
using MyApi.Entity;

namespace MyApi.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ProductEntity, Product>()
                .ReverseMap()
                .ForMember(e => e.Id, m => m.Ignore());
        }
    }
}
