using Imagen.API.Models;
using Imagen.API.Repositories;
using Imagen.DAL.Models;
using System.IO.Compression;

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
                throw new Exception ("File extension not permitted."); //TODO: excepción
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

        public Image GetImage(string id)
        {
            var image = _imageRepository.GetImage(id);
            if (image == null) throw new Exception("Imagen no existe"); //TODO: message exception
            return image;
        }

        public async Task<int> DeleteImage(string id)
        {
            return await _imageRepository.DeleteImage(id);
        }

        public string GetUntaggedImages()
        {
            var untaggedImages = _imageRepository.GetUntaggedImages();
            if (untaggedImages == null) throw new Exception("204 exception"); //TODO: Exception

            return BuildZip(untaggedImages);
        }

        public string GetAllImages()
        {
            var allImages = _imageRepository.GetAllImages();

            return BuildZip(allImages);
        }

        private string BuildZip(List<Image> allImages)
        {
            //Creamos la carpeta donde se creará el zip
            string destFileName = @"C:\images\zips\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            if (!Directory.Exists(destFileName))
            {
                Directory.CreateDirectory(destFileName);
            }

            //Llevamos las imágenes a la carpeta
            foreach (var image in allImages)
            {
                string newPath = destFileName + @"\" + image.ImageId + ".jpg";
                File.Copy(image.ImageUrl, newPath);
            }

            //Creación del zip
            string zipPath = destFileName + @"\zip.zip";
            try
            {
                ZipFile.CreateFromDirectory(destFileName, zipPath);
            }
            catch (Exception ex) { }

            return zipPath;
        }
    }
}
