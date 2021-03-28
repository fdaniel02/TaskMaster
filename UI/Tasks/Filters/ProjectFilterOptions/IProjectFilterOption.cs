using Domain.Models;

namespace UI.Tasks.Filters.ProjectFilterOptions
{
    public interface IProjectFilterOption
    {
        bool Filter(Project project, ProjectFilterArgs args);
    }
}