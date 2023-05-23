using AutoMapper;
using BestVia.Models;

namespace BestVia.API.Data
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IUsers, IUsers>();
            CreateMap<IUsers, UserView>();
            CreateMap<UserView, IUsers>();
            CreateMap<ProductType, ProductType>();
            CreateMap<Product, Product>();
            CreateMap<Category, Category>();
            CreateMap<Order, Order>();
            CreateMap<Description, Description>();
            CreateMap<Via, Via>();
            CreateMap<Key, Key>();
        }
    }

}
