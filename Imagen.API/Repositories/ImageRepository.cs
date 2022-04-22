
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
            string path = @"C:\images\" + id+".jpg";
            //File.Copy(id, path);
            try { 
            //string uploads = Path.Combine(_environment.WebRootPath, "uploads");
            // string filePath = Path.Combine(uploads, file.FileName);
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
            int l = id.Length;
            try
            {
                _dbContext.Image.Add(Image);
                await _dbContext.SaveChangesAsync();
            } catch (Exception e)
            {
            }
            
            return Image.ImageId;
        }
    }
}
