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
        /// Publica varias fotos.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPut()]
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
        /// Devuelve el id y ruta de las imágenes que no tienen ningún tag asignado. 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("untagged")]
        public IActionResult GetUntaggedImages()
        {
            var images = _imageService.GetUntaggedImages();
            if (images.Count==0) return NoContent();
            return Ok(images);
        }

        /// <summary>
        /// Devuelve todas las imágenes en .zip.
        /// </summary>
        /// <returns>Archivo .zip</returns>
        [HttpGet("GetAll/Zip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllImagesZip()
        {
            var path = _imageService.GetAllImagesZip();
            Byte[] b = System.IO.File.ReadAllBytes(path);
            //return File(b, "application/zip");
            return File(b, "application/octet-stream");
        }

        /// <summary>
        /// Obtiene todas las imágenes codificadas en Base64. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll/Base64")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageResponse))]
        public IActionResult GetAllImages()
        {
          var images = _imageService.ConvertImagesToBase64();

            return Ok(images);
        }

        /// <summary>
        /// Obtiene todas las imágenes por URL.
        /// Opción más eficiente si alojas tus imágenes en local.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll/url")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Image))]
        public IActionResult GetAllImagesByURL()
        {
            var images = _imageService.GetAllImagesByURL();

            return Ok(images);
        }
    }
}
