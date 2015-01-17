using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MonitoringPlatform.Models;
using MonitoringPlatform.Services;
using MonitoringPlatform.ViewModels;
using MonitoringPlatform.ViewModels.ObservableObjects;
using Moq;
using NUnit.Framework;

namespace MonitoringPlatform.Tests
{
    [TestFixture]
    public class UsersViewModelTests
    {
        private Mock<IUsersService> _usersServiceMock;

        private readonly List<UserModel> _users = new List<UserModel>
                                   {
                                       new UserModel {Name = "Fake User 1"},
                                       new UserModel {Name = "Fake User 2"},
                                       new UserModel {Name = "Fake User 3"}
                                   };

        private void InitUsersServiceMock()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _usersServiceMock.Setup(m => m.GetUsers()).Returns(
                async () =>
                {
                    var taskResult = Task.Run<IList<UserModel>>(() =>
                    {
                        // Simulates a long process
                        Thread.Sleep(2000);

                        // Returns data
                        return _users;
                    });
                    return await taskResult;
                });
        }

        private void SetupMapper()
        {
            Mapper.CreateMap<UserModel, UserOo>();
        }

        [SetUp]
        public void TestSetUp()
        {
            SetupMapper();
        }

        [Test]
        public void CheckThatUsersPropertyReturnsUsersFromUsersRepository()
        {
            // Arrange
            InitUsersServiceMock();
            var subject = new UsersViewModel(_usersServiceMock.Object);

            // Act
            subject.SetFocusAsync().Wait();
            var users = subject.Users;

            // Assert
            Assert.AreEqual(_users.Count, users.Count);
            Assert.AreEqual(_users[0].Name, users[0].Name);
            Assert.AreEqual(_users[1].Name, users[1].Name);
            Assert.AreEqual(_users[2].Name, users[2].Name);
        }
    }



    public class TestSyncContext : SynchronizationContext
    {
        public event EventHandler NotifyCompleted;

        public override void Post(SendOrPostCallback d, object state)
        {
            d.Invoke(state);
            NotifyCompleted(this, EventArgs.Empty);
        }
    } 

}
