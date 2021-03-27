using System.Collections.Generic;

namespace Services
{
    public class TagService : ITagService
    {
        public List<string> GetTags()
        {
            return new() { "Test1", "Test2", "TEst3", "Test3" };
        }
    }
}