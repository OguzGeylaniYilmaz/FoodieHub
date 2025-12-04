using AutoMapper;
using FoodieHub.API.Dtos.ChefDtos;
using FoodieHub.API.Dtos.ContactDtos;
using FoodieHub.API.Dtos.EventDtos;
using FoodieHub.API.Dtos.FeatureDtos;
using FoodieHub.API.Dtos.MessageDtos;
using FoodieHub.API.Dtos.ProductDtos;
using FoodieHub.API.Dtos.ServiceDtos;
using FoodieHub.API.Dtos.TestimonialDtos;
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
            CreateMap<Product, ResultProductWithCategory>().ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.CategoryName)).ReverseMap();

            CreateMap<Service, ResultServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();
            CreateMap<Service, GetByIdServiceDto>().ReverseMap();
            CreateMap<Service, UpdateServiceDto>().ReverseMap();

            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetByIdTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();

            CreateMap<Event, ResultEventDto>().ReverseMap();
            CreateMap<Event, CreateEventDto>().ReverseMap();
            CreateMap<Event, GetByIdEventDto>().ReverseMap();
            CreateMap<Event, UpdateEventDto>().ReverseMap();


            CreateMap<Chef, ResultChefDto>().ReverseMap();
            CreateMap<Chef, CreateChefDto>().ReverseMap();
            CreateMap<Chef, GetByIdChefDto>().ReverseMap();
            CreateMap<Chef, UpdateChefDto>().ReverseMap();

        }
    }
}
