using MonitoringPlatform.Repositories;
using NUnit.Framework;

namespace MonitoringPlatform.Tests
{
    [TestFixture]
    class UsersRepositoryTests
    {
        [Test]
        public void CheckThatGetUsersDoesNotReturnNull()
        {
            // Arrange
            UsersRepository subject = new UsersRepository();

            // Act
            var result = subject.GetUsers();

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
