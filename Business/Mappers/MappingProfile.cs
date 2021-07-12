using AutoMapper;
using Boilerplate.Business.DTOs;
using Boilerplate.Data.Models;


namespace Boilerplate.Business.Mappers
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

