using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Services.TagActionHandlers;

namespace Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        private readonly ITagService _tagService;

        private readonly IEnumerable<ITagActionHandler> _tagActionHandlers;

        private readonly IProjectOrderService _projectOrderService;

        public ProjectService(
            IProjectRepository projectRepository,
            ITagService tagService,
            IEnumerable<ITagActionHandler> tagActionHandlers,
            IProjectOrderService projectOrderService)
        {
            _projectRepository = projectRepository;
            _tagService = tagService;
            _tagActionHandlers = tagActionHandlers;
            _projectOrderService = projectOrderService;
        }

        public List<Project> GetProjects()
        {
            return _projectRepository.GetAll().ToList();
        }

        public List<Project> GetOpenProjects()
        {
            return _projectRepository
                            .GetAll()
                            .Where(p => p.State != ProjectStates.Closed)
                            .ToList();
        }

        public Project GetProject(int id)
        {
            return _projectRepository.GetAll().FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges(Project project)
        {
            project.LastUpdated = DateTime.Now;

            if (IsNewProject(project.ID))
            {
                AddNewProject(project);
                return;
            }

            UpdateProject(project);
        }

        public void UpdateProject(Project project)
        {
            // TODO: hack, refactor!
            if (_projectRepository.Context is not null
                && _projectRepository.Context.Entry(project).Property("State").IsModified)
            {
                var newProjectOrder = _projectRepository.GetAll().Count(p => p.State == project.State) + 1;
                project.Order = newProjectOrder;

                var originalState = (ProjectStates)_projectRepository.Context.Entry(project).Property("State").OriginalValue;
                var projectsToReorder = _projectRepository
                    .GetAll()
                    .Where(p => p.State == originalState && p.ID != project.ID)
                    .ToList();

                _projectOrderService.RefreshOrder(projectsToReorder);
            }

            _projectRepository.Update(project);
        }

        public void AddNewProject(Project project)
        {
            var newProjectOrder = _projectRepository.GetAll().Count(p => p.State == project.State) + 1;
            project.Order = newProjectOrder;
            project.Created = DateTime.Now;

            _projectRepository.Add(project);
        }

        public void AddComment(Project project, string comment)
        {
            var newComment = new Comment { Body = comment, Created = DateTime.Now };
            project.Comments.Add(newComment);
        }

        public void AddActionItem(Project project, string actionItem)
        {
            Guard.Against.Null(project?.ActionItems, nameof(project));

            var newActionItem = new ActionItem { Name = actionItem, Finished = false };
            project.ActionItems.Add(newActionItem);

            SaveChanges(project);
        }

        public void AddTag(Project project, string tagName)
        {
            var tag = _tagService.GetTagByName(tagName) ?? _tagService.CreateTag(tagName);

            var projectTag = new ProjectTags { Project = project, Tag = tag };
            project.ProjectTags.Add(projectTag);

            SaveChanges(project);

            foreach (var actionHandler in _tagActionHandlers)
            {
                actionHandler.Handle(tag, project, this);
            }
        }

        public void RemoveTag(Project project, ProjectTags tag)
        {
            project.ProjectTags.Remove(tag);

            SaveChanges(project);
        }

        public List<string> GetSources()
        {
            return _projectRepository
                .GetAll()
                .Where(p => !string.IsNullOrWhiteSpace(p.Source))
                .Select(p => p.Source)
                .Distinct()
                .ToList();
        }

        private bool IsNewProject(int projectId)
        {
            return projectId <= 0;
        }
    }
}