using Imagen.API.Models;
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

        public void PostImage(ImageRequest imageRequest)
        {
        }

        public void test()
        {
            //do things here. you can access DAL to work with those models. Then: add and save
            //_dbContext.Add(...);
            _dbContext.SaveChangesAsync();
        }
    }
}
