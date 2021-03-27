using System.Linq;
using Domain.Models;

namespace Domain.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TaskMasterContext _context;

        public TagRepository(TaskMasterContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> GetAll()
        {
            return _context.Tags.AsQueryable();
        }

        public void Add(Tag tag)
        {
            _context.Tags.AddAsync(tag);
            _context.SaveChanges();
        }
    }
}