using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using Services.Test.Fakes;
using Xunit;

namespace Services.Test
{
    public class ProjectOrderServiceTest
    {
        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(-10)]
        public void MoveUp_InvalidPosition_ThrowsException(int newPosition)
        {
            var projects = SetupBasicProjectData();
            var projectToReorder = projects.First(p => p.ID == 3);

            var sut = new ProjectOrderService(new ProjectRepositoryFake(projects));

            Action actual = () => sut.MoveUp(projectToReorder, newPosition);

            actual.Should().ThrowExactly<ArgumentException>("Invalid position");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void MoveDown_InvalidPosition_ThrowsException(int newPosition)
        {
            var projects = SetupBasicProjectData();
            var projectToReorder = projects.First(p => p.ID == 3);

            var sut = new ProjectOrderService(new ProjectRepositoryFake(projects));

            Action actual = () => sut.MoveDown(projectToReorder, newPosition);

            actual.Should().ThrowExactly<ArgumentException>("Invalid position");
        }

        [Theory]
        [MemberData(nameof(GeMoveDownData))]
        public void MoveDownTest_ReordersProjects(int projectIdToReorder, int newPosition, List<Project> expected)
        {
            var projects = SetupBasicProjectData();
            var projectToReorder = projects.First(p => p.ID == projectIdToReorder);

            var sut = new ProjectOrderService(new ProjectRepositoryFake(projects));

            sut.MoveDown(projectToReorder, newPosition);

            projects.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GeMoveUpData))]
        public void MoveUpTest_ReordersProjects(int projectIdToReorder, int newPosition, List<Project> expected)
        {
            var projects = SetupBasicProjectData();
            var projectToReorder = projects.First(p => p.ID == projectIdToReorder);

            var sut = new ProjectOrderService(new ProjectRepositoryFake(projects));

            sut.MoveUp(projectToReorder, newPosition);

            projects.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetRefreshOrderData))]
        public void RefreshOrder_RefreshesOrder(List<Project> projects, int position, List<Project> expected)
        {
            var sut = new ProjectOrderService(new ProjectRepositoryFake(projects));

            sut.RefreshOrder(projects, position);

            projects.Should().BeEquivalentTo(expected);
        }

        private List<Project> SetupBasicProjectData()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 2 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        public static IEnumerable<object[]> GeMoveUpData()
        {
            yield return new object[] { 5, 1, ExpectedResultFifthToFirst() };
            yield return new object[] { 5, 3, ExpectedResultFifthToThird() };
        }

        public static IEnumerable<object[]> GeMoveDownData()
        {
            yield return new object[] { 1, 3, ExpectedResultFirstToThird() };
            yield return new object[] { 1, 5, ExpectedResultFirstToFifth() };
            yield return new object[] { 1, 1, ExpectedResultNoChange() };
            yield return new object[] { 3, 5, ExpectedResultThirdToFifth() };
        }

        private static List<Project> ExpectedResultNoChange()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 2 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        private static List<Project> ExpectedResultFirstToThird()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 3 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 1 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 2 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        private static List<Project> ExpectedResultFirstToFifth()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 5 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 1 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 2 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        private static List<Project> ExpectedResultThirdToFifth()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 2 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        private static List<Project> ExpectedResultFifthToFirst()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 2 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 3 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 1 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        private static List<Project> ExpectedResultFifthToThird()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 2 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Backlog, Order = 1 },
            };
        }

        public static IEnumerable<object[]> GetRefreshOrderData()
        {
            yield return new object[] { InitialDataRefreshOrder1(), 1, ExpectedResultRefreshOrder() };
            yield return new object[] { InitialDataRefreshOrder2(), 1, ExpectedResultRefreshOrder() };
            yield return new object[] { InitialDataRefreshOrder3(), 1, ExpectedResultRefreshOrder() };
        }

        private static List<Project> InitialDataRefreshOrder1()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 5 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 4 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 2 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 1 },
            };
        }

        private static List<Project> InitialDataRefreshOrder2()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 10 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 9 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 77 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 10000 },
            };
        }

        private static List<Project> InitialDataRefreshOrder3()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = -5 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 0 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 0 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = -5 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 0 },
            };
        }

        private static List<Project> ExpectedResultRefreshOrder()
        {
            return new()
            {
                new() { ID = 1, Name = "Test1", State = ProjectStates.Next, Order = 1 },
                new() { ID = 2, Name = "Test2", State = ProjectStates.Next, Order = 2 },
                new() { ID = 3, Name = "Test3", State = ProjectStates.Next, Order = 3 },
                new() { ID = 4, Name = "Test3", State = ProjectStates.Next, Order = 4 },
                new() { ID = 5, Name = "Test3", State = ProjectStates.Next, Order = 5 },
            };
        }
    }
}