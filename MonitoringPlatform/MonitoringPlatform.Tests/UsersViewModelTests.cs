using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MonitoringPlatform.Models;
using MonitoringPlatform.Repositories;
using MonitoringPlatform.ViewModels;
using MonitoringPlatform.ViewModels.ObservableObjects;
using Moq;
using NUnit.Framework;

namespace MonitoringPlatform.Tests
{
    [TestFixture]
    public class UsersViewModelTests
    {
        private Mock<IUsersRepository> _usersRepositoryMock;

        private readonly List<UserModel> _users = new List<UserModel>
                                   {
                                       new UserModel {Name = "Fake User 1"},
                                       new UserModel {Name = "Fake User 2"},
                                       new UserModel {Name = "Fake User 3"}
                                   };

        private ManualResetEvent _manualResetEvent;

        private void InitUsersRepositoryMock()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _usersRepositoryMock.Setup(m => m.GetUsers()).Returns(
                () =>
                {
                    // Simulates a long running process
                    Thread.Sleep(1000);

                    // Returns a fake list
                    return _users;
                });
        }

        private void SetupMapper()
        {
            Mapper.CreateMap<UserModel, UserOo>();
        }

        private void SetupSynchronizationContext()
        {
            var context = new TestSyncContext();
            SynchronizationContext.SetSynchronizationContext(context);
            _manualResetEvent = new ManualResetEvent(false);
            context.NotifyCompleted += (sender, args) => _manualResetEvent.Set();
        }

        [SetUp]
        public void TestSetUp()
        {
            SetupMapper();
            SetupSynchronizationContext();
        }

        [Test]
        public void CheckThatUsersPropertyReturnsUsersFromUsersRepository()
        {
            // Arrange
            InitUsersRepositoryMock();
            var subject = new UsersViewModel(_usersRepositoryMock.Object);

            // Act
            var users = subject.Users;

            _manualResetEvent.WaitOne();

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
