using Imagen.API.Models;
using Imagen.API.Repositories;
using Imagen.DAL.Models;
using System.IO.Compression;
using System.Text;

namespace Imagen.API.Services
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;
        public ImageService (ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> PutImage(IFormFile file)
        {
           if (!ValidateFile(file))
            {
                throw new Exception ("File extension not permitted."); 
            }

            return await _imageRepository.PostImage(file);
        }

        public async Task PutImages(List<IFormFile> files)
        {
            if (!ValidateFiles(files))
            {
                throw new Exception("At least one file extension is not permitted."); 
            }

            await _imageRepository.PostImages(files);
        }


        private bool ValidateFile (IFormFile file)
        {
            var supportedTypes = new[] { "jpg", "png", "jpeg" };
            var fileExtension = Path.GetExtension(file.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExtension)) 
                return false;
            else return true;
        }

        private bool ValidateFiles(List<IFormFile> files)
        {
            var supportedTypes = new[] { "jpg", "png", "jpeg" };
            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExtension))
                    return false;
            }
            return true;
        }

        public Image GetImage(string id)
        {
            var image = _imageRepository.GetImage(id);
            if (image == null) throw new Exception("Imagen no existe");
            return image;
        }

        public async Task<int> DeleteImage(string id)
        {
            return await _imageRepository.DeleteImage(id);
        }

        public string GetUntaggedImagesZip()
        {
            var untaggedImages = _imageRepository.GetUntaggedImages();
            if (untaggedImages == null || untaggedImages.Count==0) throw new Exception("204 exception"); 

            return BuildZip(untaggedImages);
        }

        public string GetAllImagesZip()
        {
            var allImages = _imageRepository.GetAllImages();

            return BuildZip(allImages);
        }

        public List<Image> GetAllImagesByURL()
        {
            return _imageRepository.GetAllImages();
        }

        public ImageResponse ConvertImagesToBase64()
        {
            var allImages = _imageRepository.GetAllImages();
            var imageResponse= new ImageResponse();
            imageResponse.CodedImages = new List<SingleImage>();

            foreach (var image in allImages)
            {
                var singleImage = new SingleImage();
                byte[] imageByte = Encoding.ASCII.GetBytes(Convert.ToBase64String(File.ReadAllBytes(image.ImageUrl)));

                singleImage.ImageId = image.ImageId;
                singleImage.Base64 = imageByte;
                
                imageResponse.CodedImages.Add(singleImage);
            }
            return imageResponse;
        }

        public byte[] ConvertSingleImageToBase64(string id)
        {
            var image = _imageRepository.GetImage(id);

            byte[] imageByte = Encoding.ASCII.GetBytes(Convert.ToBase64String(File.ReadAllBytes(image.ImageUrl)));

            return imageByte;
        }

        public List<Image> GetUntaggedImages()
        {
            return _imageRepository.GetUntaggedImages();
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

        public List<Imagetagconfig> GetImageTags(string id)
        {
            return _imageRepository.GetImageTags(id);
        }
    }
}
