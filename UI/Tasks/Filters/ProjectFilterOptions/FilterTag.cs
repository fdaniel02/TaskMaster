using System;
using System.Linq;
using Ardalis.GuardClauses;
using Domain.Models;

namespace UI.Tasks.Filters.ProjectFilterOptions
{
    public class FilterTag : IProjectFilterOption
    {
        public bool Filter(Project project, ProjectFilterArgs args)
        {
            Guard.Against.Null(project, nameof(project));

            if (string.IsNullOrWhiteSpace(args?.Tag))
            {
                return true;
            }

            return project.ProjectTags.Any(t => t.Tag.Name.Equals(args.Tag, StringComparison.OrdinalIgnoreCase));
        }
    }
}