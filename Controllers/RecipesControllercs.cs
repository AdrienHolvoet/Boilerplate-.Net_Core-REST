using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Boilerplate.Business.Dtos;
using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : BaseCrudController<RecipeDto, RecipeDto, Recipe>
    {
        private readonly IRecipeService _recipeService;
        private readonly IBaseService<User> _userService;
        private readonly IBaseService<Comment> _commentService;
        private readonly IImageService _imageService;

        public RecipesController(IImageService imageService, IBaseService<Comment> commentService, IBaseService<User> userService, IMapper mapperService, ILogger<RecipesController> logger, IRecipeService service) : base(mapperService, logger, service, "Comments,Informations,Ingredients,Image")
        {
            _recipeService = service;
            _logger = logger;
            _userService = userService;
            _commentService = commentService;
            _imageService = imageService;
        }

        [HttpGet("name/{recipeTitle}")]
        public IActionResult GetRecipeByName(string recipeTitle)
        {

            try
            {
                var recipe = _recipeService.Get(recipe => recipe.Title == recipeTitle).FirstOrDefault();

                if (recipe == null)
                {
                    return NotFound("Recipe not found.");
                }

                return Ok(_mapperService.Map<RecipeDto>(recipe));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            try
            {
                var recipes = _recipeService.GetAll();

                if (recipes == null)
                {
                    return NotFound("Recipes not found.");
                }

                return Ok(_mapperService.Map<List<RecipeDto>>(recipes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("comment/{id}")]
        public IActionResult AddComment(Guid id, [FromBody] CommentDto commentRequestDto)
        {
            try
            {
                var recipe = _recipeService.Get(r => r.Id == id, false).SingleOrDefault();
                var user = _userService.GetSingle(x => x.Id == this.GetAuthenticatedIdentity());

                if (recipe == null || user == null)
                {
                    return NotFound("Recipe not found.");
                }

                var comment = _mapperService.Map<Comment>(commentRequestDto);
                comment.User = user;
                comment.RecipeId = id;
                _commentService.Add(comment);
                _commentService.SaveChanges();

                return Ok(_mapperService.Map<CommentDto>(comment));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(Guid id)
        {
            try
            {
                var oldRecipe = _recipeService.Get(e => e.Id == id, false, _includedEntites).SingleOrDefault();

                if (oldRecipe == null)
                    return NotFound("Recipe not found.");

                _recipeService.Delete(oldRecipe, true);
                //Delete directly de image because the on cascade is not set for it
                _imageService.Delete(oldRecipe.Image, true);
                _recipeService.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
