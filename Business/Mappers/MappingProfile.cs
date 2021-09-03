using AutoMapper;
using Boilerplate.Business.Dtos;
using Boilerplate.Data.Models;


namespace Boilerplate.Business.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            // CreateMap<TSource,TDestination>()

            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<User, AuthenticateResponseDto>()
              .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token));

            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<Information, InformationDto>();
            CreateMap<InformationDto, Information>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();
        }
    }
}

