using System;
using MechanicalObject.Sandbox.TodoWithSQLite.Configuration;
using MechanicalObject.Sandbox.TodoWithSQLite.Database;
using MechanicalObject.Sandbox.TodoWithSQLite.Logger;
using MechanicalObject.Sandbox.TodoWithSQLite.Model;
using MechanicalObject.Sandbox.TodoWithSQLite.Repository;

namespace MechanicalObject.Sandbox.TodoWithSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseConnectionFactory databaseConnectionFactory = new DatabaseConnectionFactory();
            IConfigurationWrapper configurationWrapper = new ConfigurationWrapper();
            ILogger logger = new ConsoleLogger();
            ITodoRepository todoRepository =  new TodoRepository(databaseConnectionFactory,configurationWrapper,logger);
            
            // create the table
            todoRepository.CreateTodoTable();
            
            // insert he first record into database 
            var todo = new Todo()
            {
                Description = "Test",
                Status = TodoStatus.ToBeDone,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Id = 1
            };

            todoRepository.AddNewTodo(todo);

            DisplayDatabaseContent(todoRepository);

            // updating the status 
            todo.Status = TodoStatus.Postponed;
            todoRepository.UpdateTodoStatus(todo);

            DisplayDatabaseContent(todoRepository);

            // deleting 
            todoRepository.DeleteTodo(todo);

            DisplayDatabaseContent(todoRepository);

            Console.Read();
        }

        private static void DisplayDatabaseContent(ITodoRepository todoRepository)
        {
            var result = todoRepository.SelectAllTodos();
            foreach (var todoInDatabase in result)
            {
                Console.WriteLine(todoInDatabase.ToString());
            }
        }
    }
}
