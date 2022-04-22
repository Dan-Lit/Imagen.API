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
        /// Devuelve una lista de fotos según los criterios especificados
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "photo1", "photo2", "etc"};
        }
        /// <summary>
        /// Obtiene una única foto, según su id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "foto con id especificado";
        }

        /// <summary>
        /// Sube una imagen.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        
        [HttpPut]
        ///<summary>
        ///Publica una única foto. 
        /// </summary>
        public async Task<string> Put(IFormFile file)
        {
            return await _imageService.PutImage(file);
        }

        // public async void PutBatch(List<IFormFile> files) { }

        /// <summary>
        /// Borra una foto según su id. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
