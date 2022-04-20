using Imagen.API.Models;
using Imagen.API.Repositories;

namespace Imagen.API.Services
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;
        public ImageService (ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        //<ImageResponse>
        public async Task PostImage(ImageRequest imageRequest)
        {
            var image = await _imageRepository.PostImage(imageRequest);
        }
    }
}
