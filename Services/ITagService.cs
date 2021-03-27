using System.Collections.Generic;
using Domain.Models;

namespace Services
{
    public interface ITagService
    {
        List<string> GetTagNames();

        Tag GetTagByName(string tagName);

        Tag CreateTag(string tagName);
    }
}