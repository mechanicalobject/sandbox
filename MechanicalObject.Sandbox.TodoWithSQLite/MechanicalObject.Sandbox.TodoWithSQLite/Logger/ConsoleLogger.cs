using System;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Logger
{
    public interface ILogger
    {
        void WriteLine(string message);
        void Write(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}
