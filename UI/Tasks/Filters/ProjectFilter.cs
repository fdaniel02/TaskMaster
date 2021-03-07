using Domain.Enums;
using Domain.Models;

namespace UI.Tasks.Filters
{
    public class ProjectFilter : IProjectFilter
    {
        public bool FilterProject(Project project, string searchExpression, bool showClosedProjects)
            => FilterSearchExpression(project, searchExpression) && FilterClosedProjects(project, showClosedProjects);

        private static bool FilterSearchExpression(Project project, string searchExpression)
        {
            if (string.IsNullOrWhiteSpace(searchExpression))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(project.Name))
            {
                return false;
            }

            return project.Name.Contains(searchExpression);
        }

        private static bool FilterClosedProjects(Project project, bool showClosedProjects)
        {
            return showClosedProjects || project.State != ProjectStates.Closed;
        }
    }
}