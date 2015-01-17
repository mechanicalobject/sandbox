using System.Collections.Generic;
using MonitoringPlatform.Models;
using MonitoringPlatform.Repositories;
using MonitoringPlatform.Services;
using Moq;
using NUnit.Framework;

namespace MonitoringPlatform.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private Mock<IUsersRepository> _usersRepositoryMock;

        private readonly List<UserModel> _users = new List<UserModel>
                                   {
                                       new UserModel {Name = "Fake User 1"},
                                       new UserModel {Name = "Fake User 2"},
                                       new UserModel {Name = "Fake User 3"}
                                   };

        private void InitUsersRepositoryMock()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _usersRepositoryMock.Setup(m => m.GetUsers()).Returns(() => this._users);
        }

        [Test]
        public void CheckThatUsersPropertyReturnsUsersFromUsersRepository()
        {
            // Arrange
            InitUsersRepositoryMock();
            UsersService subject = new UsersService(_usersRepositoryMock.Object);

            // Act
            var task = subject.GetUsers();
            var users = task.Result;

            // Assert
            Assert.AreEqual(_users.Count, users.Count);
            Assert.AreEqual(_users[0].Name, users[0].Name);
            Assert.AreEqual(_users[1].Name, users[1].Name);
            Assert.AreEqual(_users[2].Name, users[2].Name);
        }
    }

}
