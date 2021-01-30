using Prism.Events;
using TaskMaster.Domain.Models;

namespace UI.Tasks.Events
{
    public class ProjectSelectedEvent : PubSubEvent<Project>
    {
    }
}