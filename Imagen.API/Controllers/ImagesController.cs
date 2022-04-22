using Imagen.API.Models;
using Imagen.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Imagen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageService _imageService;
        public ImagesController(ImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Obtiene una única foto, según su id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Imagen solicitada </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string id) 
        {
            string path = _imageService.GetImage(id);
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "image/png");
        }

        /// <summary>
        /// Sube una imagen.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        ///<summary>
        ///Publica una única foto. 
        /// </summary>
        public async Task<string> Put(IFormFile file) 
        {
            return await _imageService.PutImage(file);
        }
        //Devuelve una lista de fotos según los criterios especificados
        // public async void PutBatch(List<IFormFile> files) { }

        /// <summary>
        /// Borra una foto según su id. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<int> Delete(string id) //Task<IActionResult>
        {
            return await _imageService.DeleteImage(id);
        }
    }
}
