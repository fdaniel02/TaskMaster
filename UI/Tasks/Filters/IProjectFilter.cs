using Domain.Models;

namespace UI.Tasks.Filters
{
    public interface IProjectFilter
    {
        bool FilterProject(Project project, string filterExpression, bool showClosedProjects);
    }
}