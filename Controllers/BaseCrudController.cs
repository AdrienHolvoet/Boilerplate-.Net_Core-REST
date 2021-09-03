using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Boilerplate.Business.Dtos;
using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Controllers
{
    public class BaseCrudController<TRequestDto, TResponseDto, TEntity> : BaseController
        where TRequestDto : BaseDto
        where TResponseDto : BaseDto
        where TEntity : BaseEntity
    {
        protected IBaseService<TEntity> _service;
        protected string _includedEntites;

        public BaseCrudController(IMapper mapperService, ILogger logger, IBaseService<TEntity> service, string includedEntities) : base(mapperService, logger)
        {
            _service = service;
            _logger = logger;
            _includedEntites = includedEntities;
        }

        // GET: api/{entity}
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            try
            {
                var list = _service.Get(null, false, _includedEntites);

                return Ok(_mapperService.Map<List<TResponseDto>>(list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        // GET api/{entity}/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id)
        {
            try
            {
                var item = _service.Get(e => e.Id == id, false, _includedEntites).SingleOrDefault();

                if (item == null)
                {
                    return NotFound($"{typeof(TEntity).Name} not found.");
                }

                return Ok(_mapperService.Map<TResponseDto>(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        // Post: api/{entity}/
        [HttpPost]
        public virtual IActionResult Post([FromBody] TRequestDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = _mapperService.Map<TEntity>(requestDto);

                _service.Add(item);
                _service.SaveChanges();

                return Ok(_mapperService.Map<TResponseDto>(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/{entity}/{id}
        [HttpPut("{id}")]
        public virtual IActionResult Put(Guid id, [FromBody] TRequestDto requestDto)
        {
            try
            {

                #region Validation

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #endregion

                requestDto.Id = id;

                var oldItem = _service.GetSingleById(id);

                if (oldItem == null)
                    return NotFound($"{typeof(TEntity).FullName} not found.");

                var newItem = _mapperService.Map<TEntity>(requestDto);

                var updatedItem = _service.Update(newItem);
                _service.SaveChanges();

                return Ok(_mapperService.Map<TResponseDto>(updatedItem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/{entity}/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                var oldItem = _service.Get(e => e.Id == id, false, _includedEntites).SingleOrDefault();

                if (oldItem == null)
                    return NotFound($"{typeof(TEntity).FullName} not found.");

                _service.Delete(oldItem, true);
                _service.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public virtual IActionResult Count()
        {
            try
            {
                return Ok(_service.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        // Post: api/categories/
        [HttpPost("collection")]
        public virtual IActionResult PostCollection([FromBody] List<TRequestDto> requestDtos)
        {
            try
            {
                #region Validation

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #endregion

                var items = _mapperService.Map<List<TEntity>>(requestDtos);

                _service.Add(items);
                _service.SaveChanges();

                return Ok(_mapperService.Map<List<TResponseDto>>(items));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

    }
}
