using System.Collections.Generic;
using Domain.Models;

namespace Services
{
    public interface IProjectOrderService
    {
        void MoveDown(Project project, int newPosition);

        void MoveUp(Project project, int newPosition);

        void RefreshOrder(List<Project> projects, int currentPosition = 1);
    }
}