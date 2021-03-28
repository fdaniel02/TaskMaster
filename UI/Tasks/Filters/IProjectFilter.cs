using Domain.Models;

namespace UI.Tasks.Filters
{
    public interface IProjectFilter
    {
        bool FilterProject(Project project, ProjectFilterArgs args);
    }
}