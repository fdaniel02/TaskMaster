using Domain.Models;

namespace Services.TagActionHandlers
{
    public class DiscussionTagCreationHandler : ITagActionHandler
    {
        public void Handle(Tag tag, Project project, ProjectService projectService)
        {
            if (!tag.Name.StartsWith("Discuss"))
            {
                return;
            }

            var contact = tag.Name.Split(' ')[1];
            var actionItemName = $"Discuss with {contact}";
            projectService.AddActionItem(project, actionItemName);
        }
    }
}