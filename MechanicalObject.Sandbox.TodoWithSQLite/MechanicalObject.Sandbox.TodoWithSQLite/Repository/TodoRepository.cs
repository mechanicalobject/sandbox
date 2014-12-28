using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MechanicalObject.Sandbox.TodoWithSQLite.Configuration;
using MechanicalObject.Sandbox.TodoWithSQLite.Database;
using MechanicalObject.Sandbox.TodoWithSQLite.Extensions;
using MechanicalObject.Sandbox.TodoWithSQLite.Logger;
using MechanicalObject.Sandbox.TodoWithSQLite.Model;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Repository
{
    public interface ITodoRepository
    {
        void CreateTodoTable();
        void AddNewTodo(Todo todoToAdd);
        List<Todo> SelectAllTodos();
        List<Todo> SelectTodosFromRange(DateTime lowerBound, DateTime upperBound);
        void UpdateTodoDescription(Todo todoToUpdate);
        void UpdateTodoStatus(Todo todoToUpdate);
        void DeleteTodo(Todo todoToDelete);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IConfigurationWrapper _configurationWrapper;
        private readonly ILogger _logger;

        public TodoRepository(IDatabaseConnectionFactory databaseConnectionFactory,IConfigurationWrapper configurationWrapper, ILogger logger)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
            _configurationWrapper = configurationWrapper;
            _logger = logger;
        }

        public void CreateTodoTable()
        {
            const string query = @"create table Todo
                         (
                         id int primary key, 
                         description nvarchar,
                         createdOn datetime default current_timestamp,
                         modifiedOn datetime default current_timestamp,
                         status int
                         )";
            try
            {
                var connectionString = _configurationWrapper.TodoSqLiteDbConnectionString;
                using (var connection =_databaseConnectionFactory.GetNewSqlLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException exception)
            {
                _logger.WriteLine(exception.Message);
            }
        }

        public void AddNewTodo(Todo todoToAdd)
        {
            try
            {
                if(todoToAdd==null)
                    throw new ArgumentNullException("todoToAdd","Cannot insert a null object into the database");
                var connectionString = _configurationWrapper.TodoSqLiteDbConnectionString;
                using (var connection =_databaseConnectionFactory.GetNewSqlLiteConnection(connectionString))
                {
                    connection.Open();
                    const string query = @"INSERT INTO Todo (Id, Description, CreatedOn, ModifiedOn,Status) VALUES 
                                      (@id, @description, @createdOn, @modifiedOn,@status)";
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    
                    command.AddParameterWithValue(@"id", todoToAdd.Id);
                    command.AddParameterWithValue(@"description", todoToAdd.Description);
                    command.AddParameterWithValue(@"status", todoToAdd.Status);
                    command.AddParameterWithValue(@"createdOn", todoToAdd.CreatedOn);
                    command.AddParameterWithValue(@"modifiedOn", todoToAdd.ModifiedOn);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException exception)
            {
                _logger.WriteLine(exception.Message);
            }
        }

        public List<Todo> SelectAllTodos()
        {
            var result = new List<Todo>();

            try
            {
                var connectionString = _configurationWrapper.TodoSqLiteDbConnectionString;
                using (var connection = _databaseConnectionFactory.GetNewSqlLiteConnection(connectionString))
                {
                    connection.Open();
                    const string query = @"Select * from Todo";
                    var selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = query;
                    var reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var todo = new Todo();
                        todo.Id = (int) reader["Id"];
                        todo.Description = (string) reader["description"];
                        todo.CreatedOn = (DateTime) reader["createdOn"];
                        todo.ModifiedOn = (DateTime) reader["modifiedOn"];
                        todo.Status = (TodoStatus) Enum.Parse(typeof (TodoStatus), reader["status"].ToString());
                        result.Add(todo);
                    }
                }
            }
            catch (SQLiteException exception)
            {
                _logger.WriteLine(exception.Message);
            }
            return result;
        }

        public List<Todo> SelectTodosFromRange(DateTime lowerBound, DateTime upperBound)
        {
            throw new NotImplementedException();
        }

        public void UpdateTodoDescription(Todo todoToUpdate)
        {
            throw new NotImplementedException();
        }

        public void UpdateTodoStatus(Todo todoToUpdate)
        {
            const string query = @"UPDATE Todo Set status=@status where id=@id";
            try
            {
                var connectionString = _configurationWrapper.TodoSqLiteDbConnectionString;
                if (todoToUpdate == null)
                    throw new ArgumentNullException("todoToUpdate",
                        "Cannot update the database with a null object!");
                using (var connection = _databaseConnectionFactory.GetNewSqlLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                     
                    var statusParameter = command.CreateParameter();
                    statusParameter.ParameterName = "@status";
                    statusParameter.Value = (int) todoToUpdate.Status;
                    command.Parameters.Add(statusParameter);

                    // this call is equivalent to :
                    // command.AddParameterWithValue("@id",todoToUpdate.Id);
                    var idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = todoToUpdate.Id;
                    command.Parameters.Add(idParameter);
                    
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException exception)
            {
                _logger.WriteLine(exception.Message);
                throw;
            }
            catch (ArgumentNullException exception)
            {
                _logger.WriteLine(exception.Message);
                throw;
            }
        }

        public void DeleteTodo(Todo todoToDelete)
        {
            const string query = @"DELETE From Todo where id=@id";
            try
            {
                var connectionString = _configurationWrapper.TodoSqLiteDbConnectionString;
                using (var connection = _databaseConnectionFactory.GetNewSqlLiteConnection(connectionString))
                {
                    connection.Open();
                    var deleteCommand = connection.CreateCommand();
                    deleteCommand.CommandType = CommandType.Text;
                    deleteCommand.CommandText = query;
                    var idParameter = deleteCommand.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = todoToDelete.Id;
                    deleteCommand.Parameters.Add(idParameter);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (SQLiteException exception)
            {
                _logger.WriteLine(exception.Message);
            }
        }
    }
}
