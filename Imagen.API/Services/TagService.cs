using System;
using System.Collections.Generic;
using System.Linq;
using Imagen.API.Repositories;


namespace Imagen.API.Services
{
    public class TagService
    {
        private readonly TagRepository _tagRepository;
        private readonly ImageRepository _imageRepository;
        public TagService(TagRepository tagRepository, ImageRepository imageRepository)
        {
            _tagRepository = tagRepository;
            _imageRepository = imageRepository; 
        }

        public async Task CreateTag(string tagName)
        {
            var tag = _tagRepository.GetAllTags().Where(p => p.Equals(tagName));
            if (tag != null) throw new Exception(); //TODO: Message exception 

            await _tagRepository.CreateTag(tagName);
        }

        public async Task AssignTag(string imageId, string tagName)
        {
            var Image = _imageRepository.GetImage(imageId);
            if (Image == null) throw new Exception(); //TODO: Message exception

            var tagList = _tagRepository.GetAllTags().Where(p => p.Equals(tagName));
            if (tagList == null) throw new Exception(); //TODO: Message exception

            await _tagRepository.AssignTag(Image, tagName);
        }
    }
}
