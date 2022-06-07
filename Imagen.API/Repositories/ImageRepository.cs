
using Imagen.DAL;
using Imagen.DAL.Context;
using Imagen.DAL.Models;
using System;

namespace Imagen.API.Repositories
{
    public class ImageRepository
    {
        private readonly ServerDbContext _dbContext;

        public ImageRepository(ServerDbContext serverDbContext)
        {
            _dbContext = serverDbContext;
        }

        public async Task<string> PostImage(IFormFile file)
        {
            string id = Guid.NewGuid().ToString();
            string path = @"C:\images\" + id + ".jpg";
            path = @"C:\Users\danie\source\repos\front-app-image\src\assets\database\" + id + ".jpg";
            try { 
            Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            } catch (Exception ex) 
            {
            }
         
            var Image = new Image()
            {
                ImageId = id,
                ImageUrl = path,
            };

            try
            {
                _dbContext.Image.Add(Image);
                await _dbContext.SaveChangesAsync();
            } catch (Exception e)
            {
            }
            
            return Image.ImageId;
        }

        public async Task PostImages(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                string id = Guid.NewGuid().ToString();
                string path = @"C:\images\" + id + ".jpg";
                path = @"C:\Users\danie\source\repos\front-app-image\src\assets\database\" + id + ".jpg";
                try
                {
                    Stream fileStream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
                catch (Exception ex)
                {
                }

                var Image = new Image()
                {
                    ImageId = id,
                    ImageUrl = path,
                };

                try
                {
                    _dbContext.Image.Add(Image);
                }
                catch (Exception e)
                {
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        public List<Image> GetUntaggedImages()
        {
            return _dbContext.Image.Where(p => p.Tagged == false).ToList();
        }

        public Image GetImage(string id)
        {
            var image = _dbContext.Image.Find(id);
            return image;
    }

        public List<Image> GetAllImages()
        {
            return _dbContext.Image.ToList();
        }

        public async Task<int> DeleteImage(string id)
        {
            var Image = new Image()
            {
                ImageId = id,
            };
            _dbContext.Attach(Image);
           _dbContext.Image.Remove(Image);
           return await _dbContext.SaveChangesAsync();
        }

        public List<Imagetagconfig> GetImageTags(string id)
        {
            var imageTagConfig = _dbContext.Imagetagconfig.Where(p => p.ImageId == id).ToList();
            return imageTagConfig;
        }


    }
}
