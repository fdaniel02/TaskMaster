using System.Linq;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITagRepository
    {
        IQueryable<Tag> GetAll();

        void Add(Tag tag);
    }
}