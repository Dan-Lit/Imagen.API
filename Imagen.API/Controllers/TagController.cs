﻿using Imagen.API.Models;
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
        /// Crea un nuevo tag. 
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
        public async Task<IActionResult> GetAllImageTagConfig()
        {
            var tags = _tagService.GetAllImageTagConfig();
            return Ok(tags);
        }

        /// <summary>
        /// Asigna un tag existente a una imagen existente. Si el tag no existe, saltará una excepción. 
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

        /// <summary>
        /// Asigna tag a varias imágenes. 
        /// </summary>
        /// <param name="request"></param>
        /// 
        [HttpPost("BatchTagging")]
        public IActionResult BatchTagging(AssignTagsRequest request)
        {
            _tagService.BatchTagging(request); 
            return Ok();
        }

    }
}
