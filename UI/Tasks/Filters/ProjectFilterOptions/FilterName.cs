using Ardalis.GuardClauses;
using Domain.Models;

namespace UI.Tasks.Filters.ProjectFilterOptions
{
    public class FilterName : IProjectFilterOption
    {
        public bool Filter(Project project, ProjectFilterArgs args)
        {
            Guard.Against.Null(project, nameof(project));

            if (string.IsNullOrWhiteSpace(args?.Name))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(project.Name))
            {
                return false;
            }

            return project.Name.Contains(args.Name, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}