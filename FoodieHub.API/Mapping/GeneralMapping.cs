using AutoMapper;
using FoodieHub.API.Dtos.ContactDtos;
using FoodieHub.API.Dtos.FeatureDtos;
using FoodieHub.API.Dtos.MessageDtos;
using FoodieHub.API.Dtos.ProductDtos;
using FoodieHub.API.Entities;

namespace FoodieHub.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdContactDto>().ReverseMap();
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();

            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();
            CreateMap<Message, ResultMessageDto>().ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();


        }
    }
}
