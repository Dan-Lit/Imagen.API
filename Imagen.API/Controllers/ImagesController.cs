using Imagen.API.Models;
using Imagen.API.Services;
using Imagen.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        /// <summary>
        /// Obtiene una única foto en base64, según su id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Imagen solicitada </returns>
        [HttpGet("{id}/blob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBase64Image(string id)
        {
            var images = _imageService.ConvertSingleImageToBase64(id);

            return Ok(images);
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

        /// <summary>
        /// Publica varias fotos.
        /// Para programadores: el nombre de los valores de FormData debe ser 'files'.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPut("several")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutImages(List<IFormFile> files)
        {
            await _imageService.PutImages(files);
            return Ok();
        }

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
        public IActionResult GetUntaggedImages()
        {
            var path = _imageService.GetUntaggedImages();
            Byte[] b = System.IO.File.ReadAllBytes(path);
            //return File(b, "application/zip");
            return File(b, "application/octet-stream");
        }

        ///// <summary>
        /// Devuelve todas las imágenes en .zip. 
        /// </summary>
        /// <returns>Archivo .zip</returns>

        [HttpGet("GetAllZip")]
        public IActionResult GetAllImagesZip()
        {
            var path = _imageService.GetAllImages();
            Byte[] b = System.IO.File.ReadAllBytes(path);
            //return File(b, "application/zip");
            return File(b, "application/octet-stream");
        }

        /// <summary>
        /// Obtiene todas las imágenes en Blob64
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageResponse))]
        public IActionResult GetAllImages()
        {
          var images = _imageService.ConvertImagesToBase64();

            return Ok(images);
        }
    }
}
