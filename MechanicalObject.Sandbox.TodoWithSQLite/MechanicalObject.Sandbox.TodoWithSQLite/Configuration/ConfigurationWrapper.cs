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
                return ConfigurationManager.ConnectionStrings["TodoDatabase"].ConnectionString;
            }
        }
    }
}
