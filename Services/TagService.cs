using System.Collections.Generic;
using System.Linq;
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

        public List<string> GetTags()
        {
            return _tagRepository
                .GetAll()
                .Select(t => t.Name)
                .ToList();
        }
    }
}