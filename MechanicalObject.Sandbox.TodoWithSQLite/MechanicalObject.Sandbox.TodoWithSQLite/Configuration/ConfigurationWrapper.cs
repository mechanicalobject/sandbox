using System;
using System.Configuration;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Configuration
{
    public interface IConfigurationWrapper
    {
        string TodoSqLiteDbConnectionString { get;  }
    }

    public class ConfigurationWrapper : IConfigurationWrapper
    {
        public string TodoSqLiteDbConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TodoDatabase"].ConnectionString;
                return connectionString;
            }
        }
    }
}
