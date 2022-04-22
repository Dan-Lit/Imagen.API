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
        public async Task<string> PutImage(IFormFile file)
        {
           if (!ValidateFile(file))
            {
                throw new Exception ("File extension not permitted.");
            }

            return await _imageRepository.PostImage(file);
        }

        private bool ValidateFile (IFormFile file)
        {
            var supportedTypes = new[] { "jpg", "png" };
            var fileExtension = Path.GetExtension(file.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExtension)) 
                return false;
            else return true;
        }

        public string GetImage(string id)
        {
            return _imageRepository.GetImage(id);
        }

        public async Task<int> DeleteImage(string id)
        {
            return await _imageRepository.DeleteImage(id);
        }
    }
}
