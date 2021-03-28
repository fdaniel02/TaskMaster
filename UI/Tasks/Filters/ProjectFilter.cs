using System.Collections.Generic;
using Domain.Models;
using UI.Tasks.Filters.ProjectFilterOptions;

namespace UI.Tasks.Filters
{
    public class ProjectFilter : IProjectFilter
    {
        private readonly IEnumerable<IProjectFilterOption> _projectFilters;

        public ProjectFilter(IEnumerable<IProjectFilterOption> projectFilters)
        {
            _projectFilters = projectFilters;
        }

        public bool FilterProject(Project project, ProjectFilterArgs args)
        {
            foreach (var filter in _projectFilters)
            {
                if (!filter.Filter(project, args))
                {
                    return false;
                }
            }

            return true;
        }
    }
}