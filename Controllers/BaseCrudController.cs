using AutoMapper;
using Boilerplate.Business.DTOs;
using Boilerplate.Data.Models;

namespace Boilerplate.Controllers
{
    public class BaseCrudController<TRequestDto, TResponseDto, TEntity> : BaseController
        where TRequestDto : BaseDto
        where TResponseDto : BaseDto
        where TEntity : BaseEntity
    {
        public BaseCrudController(IMapper mapperService) : base(mapperService)
        {
        }
    }
}