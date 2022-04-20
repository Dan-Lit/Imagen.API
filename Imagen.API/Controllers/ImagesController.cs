using Imagen.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Imagen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

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
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] ImageRequest imageRequest)
        {
            var imageResponse = await _imageService.PutImage(imageRequest);

            return Ok(imageResponse);
        }

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
