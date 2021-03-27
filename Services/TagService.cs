using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public List<string> GetTagNames()
        {
            return _tagRepository
                .GetAll()
                .Select(t => t.Name)
                .ToList();
        }

        public Tag GetTagByName(string tagName)
        {
            return _tagRepository.GetAll().FirstOrDefault(t => t.Name == tagName);
        }

        public Tag CreateTag(string tagName)
        {
            var tag = new Tag { Name = tagName };

            _tagRepository.Add(tag);

            return tag;
        }
    }
}