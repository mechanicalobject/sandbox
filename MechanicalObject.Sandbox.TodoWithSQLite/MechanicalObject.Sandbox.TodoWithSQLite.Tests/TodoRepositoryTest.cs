using System;
using System.Data;
using System.Data.SQLite;
using MechanicalObject.Sandbox.TodoWithSQLite.Configuration;
using MechanicalObject.Sandbox.TodoWithSQLite.Database;
using MechanicalObject.Sandbox.TodoWithSQLite.Logger;
using MechanicalObject.Sandbox.TodoWithSQLite.Repository;
using Moq;
using NUnit.Framework;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Tests
{
    [TestFixture]
    public class TodoRepositoryTest
    {
        private Mock<IDatabaseConnectionFactory> _databaseConnectionFactoryMock;
        private Mock<IConfigurationWrapper> _configurationWrapperMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IDbConnection> _sqLiteConnectionMock;
        const string ConnectionString = "default connection string";
        [SetUp]
        public void Init()
        {
            _databaseConnectionFactoryMock = new Mock<IDatabaseConnectionFactory>();
            _configurationWrapperMock = new Mock<IConfigurationWrapper>();
            _loggerMock = new Mock<ILogger>();
            _sqLiteConnectionMock = new Mock<IDbConnection>();
        }

        [TearDown]
        public void Clean()
        {
            _databaseConnectionFactoryMock = null;
            _configurationWrapperMock = null;
            _loggerMock = null;
            _sqLiteConnectionMock = null;
        }

        [Test]
        public void CreateTodoTable_SystemShouldLogIfOpeningConnectionFails()
        {
            // ==> Arrange 

            // fake the fail while opening connection
            _sqLiteConnectionMock.Setup(m=>m.Open()).Throws<SQLiteException>();
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            sut.CreateTodoTable();

            // ==> Assert
            _loggerMock.Verify(m=>m.WriteLine(It.IsAny<string>()),Times.Once());
        }

        [Test]
        public void CreateTodoTable_SystemShouldLogIfCreatingACommandFails()
        {
            // ==> Arrange 

            // fake the fail while creating the command
            _sqLiteConnectionMock.Setup(m => m.CreateCommand()).Throws<SQLiteException>();
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            sut.CreateTodoTable();

            // ==> Assert
            _loggerMock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void CreateTodoTable_SystemShouldLogIfExecutionTheCommandFails()
        {
            // ==> Arrange 

            // fake the fail while executing the query
            var sqlCommandMock = new Mock<IDbCommand>();
            sqlCommandMock.Setup(m => m.ExecuteNonQuery()).Throws<SQLiteException>();

            _sqLiteConnectionMock.Setup(m => m.CreateCommand()).Returns(sqlCommandMock.Object);
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            sut.CreateTodoTable();

            // ==> Assert
            _loggerMock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void CreateTodoTable_CheckIfConnectionIsDisposedAfterTheMethodIsCalled()
        {
            // ==> Arrange 

            // fake the fail while executing the query
            var sqlCommandMock = new Mock<IDbCommand>();
            _sqLiteConnectionMock.Setup(m => m.CreateCommand()).Returns(sqlCommandMock.Object);
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            sut.CreateTodoTable();

            // ==> Assert
            _sqLiteConnectionMock.Verify(m=>m.Dispose(), Times.Once());
        }

        [Test]
        public void AddNewTodo_TodoToAddShouldNotBeNull()
        {
            // fake the fail while executing the query
            var sqlCommandMock = new Mock<IDbCommand>();
            _sqLiteConnectionMock.Setup(m => m.CreateCommand()).Returns(sqlCommandMock.Object);
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            Assert.That(() => sut.AddNewTodo(null),
                Throws.
                Exception.
                TypeOf<ArgumentNullException>());
        }


        [Test]
        public void UpdateStatus_TodoToUpdateShouldNotBeNull()
        {
            // ==> Arrange 

            // fake the fail while executing the query
            var sqlCommandMock = new Mock<IDbCommand>();
            _sqLiteConnectionMock.Setup(m => m.CreateCommand()).Returns(sqlCommandMock.Object);
            _configurationWrapperMock.Setup(m => m.TodoSqLiteDbConnectionString).Returns(ConnectionString);
            _databaseConnectionFactoryMock.Setup(m => m.GetNewSqlLiteConnection(ConnectionString)).Returns(_sqLiteConnectionMock.Object);

            // get concrete objects
            var databaseConnectionFactory = _databaseConnectionFactoryMock.Object;
            var configurationWrapper = _configurationWrapperMock.Object;
            var logger = _loggerMock.Object;

            // create the system under test
            var sut = new TodoRepository(databaseConnectionFactory, configurationWrapper, logger);

            // ==> Act
            Assert.That(()=>sut.UpdateTodoStatus(null),
                Throws.
                Exception.
                TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Dummy()
        {
            // D:\Users\MechanicalObject\AppData\Roaming
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Console.WriteLine("Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData ==> {0}", appdata);
            var dataDirectoryBeforeAffecting = AppDomain.CurrentDomain.GetData("DataDirectory");
            Console.WriteLine(@"AppDomain.CurrentDomain.GetData('DataDirectory') ==> {0}", dataDirectoryBeforeAffecting);
            AppDomain.CurrentDomain.SetData("DataDirectory",@"D:\Test");
            var dataDirectoryAfterAffecting = AppDomain.CurrentDomain.GetData("DataDirectory");
            Console.WriteLine(@"AppDomain.CurrentDomain.GetData('DataDirectory') ==> {0}", dataDirectoryAfterAffecting);
        }
    }
}
