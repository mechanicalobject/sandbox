using System.Data;
using System.Data.SQLite;
using MechanicalObject.Sandbox.TodoWithSQLite.Extensions;
using NUnit.Framework;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Tests
{
    [TestFixture]
    public class DbCommandExtensionsTest
    {
        [Test]
        public void TestAddParameterWithValue()
        {
            IDbCommand command = new SQLiteCommand();
            Assert.AreEqual(0, command.Parameters.Count);
            command.AddParameterWithValue("@id", 1);
            Assert.AreEqual(1,command.Parameters.Count);
            
            Assert.AreEqual(1, ((SQLiteParameter)command.Parameters[0]).Value);
            Assert.AreEqual("@id", ((SQLiteParameter)command.Parameters[0]).ParameterName);
        }
    }
}
