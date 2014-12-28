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
            throw new System.NotImplementedException();
        }

        public void Write(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
