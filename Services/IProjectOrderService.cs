using Domain.Models;

namespace Services
{
    public interface IProjectOrderService
    {
        void MoveDown(Project project, int newPosition);

        void MoveUp(Project project, int newPosition);
    }
}