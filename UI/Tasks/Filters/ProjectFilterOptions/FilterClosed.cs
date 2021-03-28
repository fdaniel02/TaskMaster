using Ardalis.GuardClauses;
using Domain.Enums;
using Domain.Models;

namespace UI.Tasks.Filters.ProjectFilterOptions
{
    public class FilterClosed : IProjectFilterOption
    {
        public bool Filter(Project project, ProjectFilterArgs args)
        {
            Guard.Against.Null(project, nameof(project));

            return args is null || args.ShowClosed || project.State != ProjectStates.Closed;
        }
    }
}