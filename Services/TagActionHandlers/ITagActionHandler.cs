using Domain.Models;

namespace Services.TagActionHandlers
{
    public interface ITagActionHandler
    {
        void Handle(Tag tag, Project project, ProjectService projectService);
    }
}