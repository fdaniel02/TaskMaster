using Domain.Models;
using Prism.Events;

namespace UI.Tasks.Events
{
    public class ProjectSelectedEvent : PubSubEvent<Project>
    {
    }
}