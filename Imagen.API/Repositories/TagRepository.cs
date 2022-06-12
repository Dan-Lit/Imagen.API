using Imagen.DAL.Context;
using Imagen.DAL.Models;

namespace Imagen.API.Repositories
{
    public class TagRepository
    {
        private readonly ServerDbContext _dbContext;
        public TagRepository (ServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateTag(string tagName)
        {
            var Tag = new Tag()
            {
                TagId = tagName
            };

            _dbContext.Tag.Add(Tag);
            await _dbContext.SaveChangesAsync(); 
        }

        public List<string> GetAllTags()
        {
            return _dbContext.Tag.Select(p => p.TagId).ToList();
        }

        public List<Imagetagconfig> GetAllImageTagConfig()
        {
            return _dbContext.Imagetagconfig.ToList();
        }

        internal async Task AssignTag(Image image, string tagName)
        {
            var imagetagconfig = new Imagetagconfig()
            {
                ImageId = image.ImageId,
                TagId = tagName,
            };

            image.Tagged = true;

            _dbContext.Image.Update(image);
            _dbContext.Imagetagconfig.Add(imagetagconfig);

            await _dbContext.SaveChangesAsync();
        }
    }
}
