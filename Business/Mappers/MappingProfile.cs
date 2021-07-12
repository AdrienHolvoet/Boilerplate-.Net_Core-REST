using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Data.Models;


namespace Boilerplate_REST.Business.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<TSource,TDestination>()

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
        }
    }
}

