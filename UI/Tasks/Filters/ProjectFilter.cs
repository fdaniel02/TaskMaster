using System;
using System.Linq;
using Domain.Enums;
using Domain.Models;

namespace UI.Tasks.Filters
{
    public class ProjectFilter : IProjectFilter
    {
        public bool FilterProject(Project project, string searchExpression, string tagFilter, bool showClosedProjects)
        {
            if (!FilterSearchExpression(project, searchExpression))
            {
                return false;
            }

            if (!FilterTag(project, tagFilter))
            {
                return false;
            }

            return FilterClosedProjects(project, showClosedProjects);
        }

        // TODO: move these to separate classes
        private bool FilterSearchExpression(Project project, string searchExpression)
        {
            if (string.IsNullOrWhiteSpace(searchExpression))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(project.Name))
            {
                return false;
            }

            return project.Name.Contains(searchExpression, System.StringComparison.OrdinalIgnoreCase);
        }

        private bool FilterTag(Project project, string tagFilter)
        {
            if (string.IsNullOrWhiteSpace(tagFilter))
            {
                return true;
            }

            return project.ProjectTags.Any(t => t.Tag.Name.Equals(tagFilter, StringComparison.OrdinalIgnoreCase));
        }

        private bool FilterClosedProjects(Project project, bool showClosedProjects)
        {
            return showClosedProjects || project.State != ProjectStates.Closed;
        }
    }
}