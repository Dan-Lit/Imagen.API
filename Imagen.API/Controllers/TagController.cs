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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTag(string tagName)
        {
            await _tagService.CreateTag(tagName);
            return Ok();
        }

        /// <summary>
        /// Asigna un tag existente a una imagen existente. Si el tag no existe, saltará una excepción. 
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignTag(string imageId, string tagName)
        {
            await _tagService.AssignTag(imageId, tagName);
            return Ok();
        }
    }
}
