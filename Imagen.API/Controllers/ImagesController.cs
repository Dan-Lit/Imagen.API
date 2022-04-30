using Imagen.API.Models;
using Imagen.API.Services;
using Imagen.DAL.Models;
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
        public IActionResult GetImage(string id)
        {
            string path = _imageService.GetImage(id).ImageUrl;
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "image/png");
        }

        ///<summary>
        ///Publica una única foto. 
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> PutImage(IFormFile file)
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
        public async Task<IActionResult> DeleteImage(string id)
        {
            await _imageService.DeleteImage(id);
            return Ok();
        }

        /// <summary>
        /// Devuelve las imágenes que no tienen ningún tag asignado. 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public List<Image> GetUntaggedImages()
        {
            return _imageService.GetUntaggedImages();
        }

        /// <summary>
        /// Devuelve todas las imágenes. 
        /// </summary>
        /// <returns>Archivo .zip</returns>

        [HttpPost("GetAll")]
        public IActionResult GetAllImages()
        {
             var path = _imageService.GetAllImages();
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "application/zip");
        }
    }
}
