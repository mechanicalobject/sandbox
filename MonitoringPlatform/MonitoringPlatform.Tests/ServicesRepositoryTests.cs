using MonitoringPlatform.Repositories;
using NUnit.Framework;

namespace MonitoringPlatform.Tests
{
    [TestFixture]
    public class ServicesRepositoryTests
    {
        [Test]
        public void CheckThatGetServicesDoesNotReturnNull()
        {
            // Arrange
            ServicesRepository subject = new ServicesRepository();

            // Act
            var result = subject.GetServices();

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
