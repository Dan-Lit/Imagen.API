using System;
using System.Collections.Generic;
using System.Linq;
using Imagen.API.Models;
using Imagen.API.Repositories;
using Imagen.DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
            var tag = _tagRepository.GetAllTags().Where(p => p.Equals(tagName)).ToList();
            if (tag.Count>0) throw new Exception("El tag ya existe"); //TODO: Message exception 

            await _tagRepository.CreateTag(tagName);
        }

        public async Task AssignTag(string imageId, string tagName)
        {
            var image = _imageRepository.GetImage(imageId);
            if (image == null) throw new Exception("Imagen no encontrada"); //TODO: Message exception

            var tagList = _tagRepository.GetAllTags().Where(p => p.Equals(tagName));
            if (tagList == null) throw new Exception("El tag no existe"); //TODO: Message exception

            await _tagRepository.AssignTag(image, tagName);
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }

        public async Task BatchTagging(AssignTagsRequest request)
        {
            foreach (var r in request.Tags)
            {
                await AssignTag(r.Key, r.Value);
            }
        }
    }
}
