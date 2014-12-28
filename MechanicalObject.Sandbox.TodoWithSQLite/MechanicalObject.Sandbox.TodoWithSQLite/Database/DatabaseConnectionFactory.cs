using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Database
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetNewSqlLiteConnection(string connectionString);
    }

    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        public IDbConnection GetNewSqlLiteConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
