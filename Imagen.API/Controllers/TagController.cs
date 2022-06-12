using Imagen.API.Models;
using Imagen.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Imagen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly TagService _tagService;
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Crea un nuevo tag. 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [HttpGet("{tagName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTag(string tagName)
        {
            await _tagService.CreateTag(tagName);
            return Ok();
        }

        /// <summary>
        /// Obtiene el nombre de todos los tags creados 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = _tagService.GetAllTags();
            return Ok(tags);
        }

        /// <summary>
        /// Obtén la relación entre tags e imágenes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("imagetagconfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllImageTagConfig()
        {
            var tags = _tagService.GetAllImageTagConfig();
            if (tags.Count == 0) return NoContent();
            return Ok(tags);
        }

        /// <summary>
        /// Asigna un tag existente a una imagen existente. 
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [HttpGet("{imageId}/{tagName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignTag(string imageId, string tagName)
        {
            await _tagService.AssignTag(imageId, tagName);
            return Ok();
        }
    }
}
